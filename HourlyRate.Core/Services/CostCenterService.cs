using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Models.GeneralCost;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Common;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.CostCategories;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Core.Services
{
    public class CostCenterService : ICostCenterService
    {
        private readonly IRepository _repo;
        private readonly ApplicationDbContext _context;

        public CostCenterService(
            IRepository repo
            , ApplicationDbContext context
        )
        {
            _context = context;
            _repo = repo;
        }


        public async Task<IEnumerable<GeneralCostCenterViewModel>> AllCostTypes()
        {

            return await _repo.AllReadonly<CostCategory>()
                .OrderBy(c => c.Name)
                .Select(c => new GeneralCostCenterViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }



        public async Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments()
        {
            return await _repo.AllReadonly<Department>()
                .OrderBy(c => c.Name)
                .Select(c => new EmployeeDepartmentModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task AddCostCenter(AddCostCenterViewModel ccModel, Guid companyId)
        {
            var activeYearId = ActiveFinancialYearId();



            var employeeNo = _repo.AllReadonly<Employee>()
                .Count(e => e.DepartmentId == ccModel.DepartmentId);

            var employeSalary = _repo.AllReadonly<Expenses>()
                .Where(e => e.Employee!.Department!.Id == ccModel.DepartmentId)
                .Sum(e => e.Amount);


            var costCenter = new CostCenter()
            {
                Name = ccModel.Name,
                FloorSpace = ccModel.FloorSpace,
                AvgPowerConsumptionKwh = ccModel.AvgPowerConsumptionKwh,
                AnnualHours = ccModel.AnnualHours,
                AnnualChargeableHours = ccModel.AnnualChargeableHours,
                DepartmentId = ccModel.DepartmentId,
                IsUsingWater = ccModel.IsUsingWater,
                DirectAllocatedStuff = employeeNo,
                DirectWagesCost = employeSalary,
                CompanyId = companyId,
                FinancialYearId = activeYearId,

            };

            await _repo.AddAsync(costCenter);
            await _repo.SaveChangesAsync();


        }

        public async Task AddCostCenterToEmployee(AddCostCenterViewModel ccModel)
        {
            var getCostCenter = _repo.AllReadonly<CostCenter>()
                .First(cc => cc.Name == ccModel.Name);

            var employeeExpenses = _context.Expenses
                .Where(e => e.Employee!.Department!.Id == getCostCenter.DepartmentId);

            foreach (var e in employeeExpenses)
            {
                e.CostCenterId = getCostCenter.Id;
            }

            _context.UpdateRange(employeeExpenses);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Same for all services, add it to a general service
        /// </summary>
        /// <returns></returns>
        private int ActiveFinancialYear()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Year;
        }
        private int ActiveFinancialYearId()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Id;
        }

        public async Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId)
        {
            await UpdateAllCostCenters(companyId);

            var allCostCentersUpdated = _context.CostCenters
                .Where(c => c.CompanyId == companyId && c.Name != "None")
                .Select(c => new CostCenterViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    FloorSpace = c.FloorSpace,
                    AvgPowerConsumptionKwh = c.AvgPowerConsumptionKwh,
                    AnnualHours = c.AnnualHours,
                    AnnualChargeableHours = c.AnnualChargeableHours,
                    DepartmentId = c.DepartmentId,
                    TotalPowerConsumption = c.TotalPowerConsumption,
                    DirectAllocatedStuff = c.DirectAllocatedStuff,
                    DirectWagesCost = c.DirectWagesCost,
                    DirectRepairCost = c.DirectRepairCost,
                    DirectDepreciationCost = c.DirectDepreciationCost,
                    DirectElectricityCost = c.DirectElectricityCost,
                    RentCost = c.RentCost,
                    TotalDirectCosts = c.TotalDirectCosts,
                    IndirectHeatingCost = c.IndirectHeatingCost,
                    TotalIndex = c.TotalIndex,
                    WaterTotalIndex = c.WaterTotalIndex,
                    IndirectWaterCost = c.IndirectWaterCost,
                    IndirectTaxes = c.IndirectTaxes,
                    IndirectPhonesCost = c.IndirectPhonesCost,
                    IndirectAdministrationWagesCost = c.IndirectAdministrationWagesCost,
                    IndirectOtherCost = c.IndirectOtherCost,
                    IndirectMaintenanceWagesCost = c.IndirectMaintenanceWagesCost,
                    IndirectDepreciationCost = c.IndirectDepreciationCost,
                    IndirectTotalCosts = c.IndirectTotalCosts,
                    TotalCosts = c.TotalCosts,
                    WagesPerMonth = c.WagesPerMonth,
                    MachinesPerMonth = c.MachinesPerMonth,
                    OverheadsPerMonth = c.OverheadsPerMonth,
                    WagesPerHour = c.WagesPerHour,
                    MachinesPerHour = c.MachinesPerHour,
                    OverheadsPerHour = c.OverheadsPerHour,
                    TotalHourlyCostRate = c.TotalHourlyCostRate,
                }).ToListAsync();
            return await allCostCentersUpdated;
        }

        private async Task UpdateAllCostCenters(Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var allCostCenters = _context.CostCenters
                .Where(c =>
                            c.CompanyId == companyId &&
                            c.Name != "None" &&
                            c.FinancialYearId == activeFinancialYearId).ToList();

            var allExpenses = _context.Expenses;
            var totalSalaryMaintenance = TotalSalaryMaintenanceDepartment(allExpenses);

            foreach (var costCenter in allCostCenters)
            {
                var totalDirectCostSum = 0.0m;
                var totalIndirectCostSum = 0.0m;
                var currentCostCenterId = costCenter.Id;


                //----------- Employees count wages
                CurrentEmployeeCount(allExpenses, currentCostCenterId, activeFinancialYearId, costCenter);

                //---------- EmployeesWages
                totalDirectCostSum += CurrentCostCenterEmployeesWagesSum(allExpenses, currentCostCenterId,
                    activeFinancialYearId, costCenter);

                //----------- Consumables
                totalDirectCostSum += CurrentCostCenterConsumablesTotal(allExpenses, currentCostCenterId,
                    activeFinancialYearId, costCenter);

                //---------- Repair
                //TODO: All 10 predefined CostCategories can be Switched, but will not follow vertical flow of the View

                totalDirectCostSum += CurrentCostCenterDirectRepairSum(allExpenses, currentCostCenterId,
                    activeFinancialYearId, costCenter, 7);

                //----------- Direct Depreciation
                totalDirectCostSum += CurrentCostCenterDepreciationSum(allExpenses, currentCostCenterId,
                    activeFinancialYearId, costCenter, 8);

                //-------------Rent
                var totalRentSpace = TotalRentSpace(allCostCenters);

                var rentCost = RentCostTotal(allExpenses, 6);

                totalIndirectCostSum += CurrentCostCenterRent(rentCost, totalRentSpace, costCenter);

                //-----------Electricity
                var totalElectricCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 2);

                costCenter.TotalPowerConsumption =
                    costCenter.AnnualChargeableHours * costCenter.AvgPowerConsumptionKwh;

                var electricityPricePerKwhIndirectlyCalculated =
                    ElectricityPricePerKwhIndirectlyCalculated(totalElectricCost, allCostCenters);

                costCenter.DirectElectricityCost =
                    costCenter.TotalPowerConsumption * electricityPricePerKwhIndirectlyCalculated;

                totalIndirectCostSum += costCenter.DirectElectricityCost;

                //---------Heating
                var heatingCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 9);

                var heatingPerSqM = heatingCost / totalRentSpace;
                costCenter.IndirectHeatingCost = costCenter.FloorSpace * heatingPerSqM;
                totalIndirectCostSum += costCenter.FloorSpace * heatingPerSqM;

                //--------Total Direct Cost
                costCenter.TotalDirectCosts = totalDirectCostSum;

                //--------Total Index - Total Direct Cost / Current Total Direct cost
                var sumTotalDirectCosts = SumTotalDirectCosts(allCostCenters);
                costCenter.TotalIndex = sumTotalDirectCosts / costCenter.TotalDirectCosts;


                //----WaterIndex - Total Direct Of CC Using Water / Current Total Direct 

                var tDirectCostOfCcUsingWater = allCostCenters.Where(s => s.IsUsingWater == true)
                    .Sum(s => s.TotalDirectCosts);
                var totalWaterCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 1);

                totalIndirectCostSum += SetWaterCost(costCenter, tDirectCostOfCcUsingWater, totalWaterCost);

                //-------Taxes
                var taxCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 10);
                costCenter.IndirectTaxes = taxCosts / costCenter.TotalIndex;

                // ---- Phones
                var phonesCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 3);
                costCenter.IndirectPhonesCost = phonesCosts / costCenter.TotalIndex;
                totalIndirectCostSum += costCenter.IndirectPhonesCost;

                // -----Other
                var otherCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 4);
                costCenter.IndirectOtherCost = otherCosts / costCenter.TotalIndex;
                totalIndirectCostSum += costCenter.IndirectOtherCost;

                //------General Administration
                var administrationCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 5);
                costCenter.IndirectAdministrationWagesCost = administrationCost / costCenter.TotalIndex;
                totalIndirectCostSum += costCenter.IndirectAdministrationWagesCost;


                //---------- EmployeesMaintenanceWages
                costCenter.IndirectMaintenanceWagesCost =
                    totalSalaryMaintenance / costCenter.TotalIndex;
                totalIndirectCostSum += costCenter.IndirectMaintenanceWagesCost;

                //---------- Indirect Depreciation
                var indirectDepreciationCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 11);
                costCenter.IndirectDepreciationCost = indirectDepreciationCost / costCenter.TotalIndex;
                totalIndirectCostSum += costCenter.IndirectDepreciationCost;

                //--------- Total Costs

                costCenter.TotalCosts = costCenter.TotalDirectCosts + costCenter.IndirectTotalCosts;

                costCenter.IndirectTotalCosts = totalIndirectCostSum;

                //-------- Wages per Month
                costCenter.WagesPerMonth =
                    (costCenter.DirectWagesCost +
                     costCenter.IndirectAdministrationWagesCost +
                     costCenter.IndirectMaintenanceWagesCost) / 12;

                //--------Machine per Month
                costCenter.MachinesPerMonth =
                    (costCenter.DirectRepairCost +
                    costCenter.DirectGeneraConsumablesCost +
                    costCenter.DirectDepreciationCost +
                    costCenter.IndirectDepreciationCost) / 12;

                //---------- Overheads per Month
                //TODO: -----missing indirect consumables
                costCenter.OverheadsPerMonth =
                    (costCenter.DirectGeneraConsumablesCost +
                     costCenter.RentCost +
                     costCenter.DirectElectricityCost +
                     costCenter.IndirectHeatingCost +
                     costCenter.IndirectWaterCost +
                     costCenter.IndirectTaxes +
                     costCenter.IndirectPhonesCost +
                     costCenter.IndirectOtherCost) / 12;

                //-------- Wages per Hour
                costCenter.WagesPerHour =
                    (costCenter.DirectWagesCost +
                     costCenter.IndirectAdministrationWagesCost +
                     costCenter.IndirectMaintenanceWagesCost) / costCenter.AnnualChargeableHours;

                //--------Machine per Hour
                costCenter.MachinesPerHour =
                    (costCenter.DirectRepairCost +
                     costCenter.DirectGeneraConsumablesCost +
                     costCenter.DirectDepreciationCost +
                     costCenter.IndirectDepreciationCost) / costCenter.AnnualChargeableHours;

                //---------- Overheads per Hour
                //TODO: -----missing indirect consumables
                costCenter.OverheadsPerHour =
                    (costCenter.DirectGeneraConsumablesCost +
                     costCenter.RentCost +
                     costCenter.DirectElectricityCost +
                     costCenter.IndirectHeatingCost +
                     costCenter.IndirectWaterCost +
                     costCenter.IndirectTaxes +
                     costCenter.IndirectPhonesCost +
                     costCenter.IndirectOtherCost) / costCenter.AnnualChargeableHours;

                _context.CostCenters.Update(costCenter);
            }


            await _context.SaveChangesAsync();
        }

        private decimal TotalSalaryMaintenanceDepartment(DbSet<Expenses> allExpenses)
        {
            var employeesFromMaintenanceDept = _context.Employees
                .Where(e => e.Department.Name == "Maintenance").ToList();
            var totalSalaryMaintenance = 0.0m;
            foreach (var employee in employeesFromMaintenanceDept)
            {
                totalSalaryMaintenance += allExpenses
                    .First(e => e.EmployeeId == employee.Id).Amount;
            }

            return totalSalaryMaintenance;
        }

        private static decimal GetSumOfTotalIndirectCostOfCc(DbSet<Expenses> allExpenses, int activeFinancialYearId, int costCategoryId)
        {
            var totalIndirectCost = allExpenses
                .Where(c => c.CostCategoryId == costCategoryId
                            && c.FinancialYearId == activeFinancialYearId)
                .Select(r => r.Amount).Sum();
            return totalIndirectCost;
        }
        private static decimal SumTotalDirectCosts(List<CostCenter> allCostCenters)
        {
            var sumTotalDirectCosts = allCostCenters.Sum(s => s.TotalDirectCosts);
            return sumTotalDirectCosts;
        }

        private static decimal SetWaterCost(CostCenter currentCostCenter, decimal tDirectCostOfCcUsingWater,
            decimal totalWaterCost)
        {
            if (currentCostCenter.IsUsingWater)
            {
                currentCostCenter.WaterTotalIndex = tDirectCostOfCcUsingWater / currentCostCenter.TotalDirectCosts;
                return currentCostCenter.IndirectWaterCost =
                    totalWaterCost / currentCostCenter.WaterTotalIndex;

            }

            currentCostCenter.WaterTotalIndex = 0;
            return currentCostCenter.IndirectWaterCost = 0;
        }



        private static decimal ElectricityPricePerKwhIndirectlyCalculated(decimal totalElectricCost, List<CostCenter> allCostCenters)
        {
            var electricityPricePerKwhIndirectlyCalculated = totalElectricCost /
                                                             allCostCenters.Select(tp => tp.TotalPowerConsumption).Sum();
            return electricityPricePerKwhIndirectlyCalculated;
        }


        private static decimal CurrentCostCenterRent(decimal rentCost, decimal totalRentSpace, CostCenter currentCostCenter)
        {

            var rentPerSqM = rentCost / totalRentSpace;

            return currentCostCenter.RentCost = currentCostCenter.FloorSpace * rentPerSqM;
        }

        private static decimal RentCostTotal(DbSet<Expenses> allExpenses, int costCategoryId)
        {
            var rentCost = allExpenses
                .Where(c => c.CostCategoryId == costCategoryId)
                .Select(r => r.Amount).Sum();
            return rentCost;
        }

        /// <summary>
        /// All CostCenter Rented Space in m2
        /// </summary>
        /// <param name="allCostCenters"></param>
        /// <returns>Return Rented Space in m2</returns>
        private static decimal TotalRentSpace(List<CostCenter> allCostCenters)
        {
            var totalRentSpace = allCostCenters.Select(r => r.FloorSpace).Sum();
            return totalRentSpace;
        }

        private static decimal CurrentCostCenterDepreciationSum(DbSet<Expenses> allExpenses, int currentCostCenterId,
            int activeFinancialYearId, CostCenter currentCostCenter, int costCategoryId)
        {

            var directGeneraDepreciationCost = allExpenses
                .Where(c => c.CostCenterId == currentCostCenterId && c.CostCategoryId == costCategoryId
                                                                  && c.FinancialYearId == activeFinancialYearId)
                .Select(r => r.Amount).Sum();

            currentCostCenter.DirectDepreciationCost = directGeneraDepreciationCost;
            decimal totalDirectCostSum = directGeneraDepreciationCost;

            return totalDirectCostSum;
        }

        /// <summary>
        /// Calculate All Cost of Repairs Directly assign to Current Cost Center
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenterId"></param>
        /// <param name="activeFinancialYearId"></param>
        /// <param name="currentCostCenter"></param>
        /// <param name="costCategoryId"></param>
        /// <returns>Return Sum</returns>
        private static decimal CurrentCostCenterDirectRepairSum(DbSet<Expenses> allExpenses, int currentCostCenterId,
            int activeFinancialYearId, CostCenter currentCostCenter, int costCategoryId)
        {
            var directRepairCost = allExpenses
                .Where(c => c.CostCenterId == currentCostCenterId && c.CostCategoryId == costCategoryId
                                                                  && c.FinancialYearId ==
                                                                  activeFinancialYearId)
                .Select(r => r.Amount).Sum();
            currentCostCenter.DirectRepairCost = directRepairCost;
            decimal totalDirectCostSum = directRepairCost;
            return totalDirectCostSum;
        }

        /// <summary>
        /// Calculate All Cost of Consumables assign to Current Cost Center
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenterId"></param>
        /// <param name="activeFinancialYearId"></param>
        /// <param name="currentCostCenter"></param>
        /// <returns>Return Sum</returns>
        private static decimal CurrentCostCenterConsumablesTotal(DbSet<Expenses> allExpenses, int currentCostCenterId,
            int activeFinancialYearId, CostCenter currentCostCenter)
        {
            var currentCostGeneralConsumables = allExpenses
                .Where(c => c.CostCenterId == currentCostCenterId &&
                            c.ConsumableId != null &&
                            c.FinancialYearId == activeFinancialYearId);

            currentCostCenter.DirectGeneraConsumablesCost = currentCostGeneralConsumables.Sum(c => c.Amount);
            decimal totalDirectCostSum = currentCostCenter.DirectGeneraConsumablesCost;
            return totalDirectCostSum;
        }

        private static decimal CurrentCostCenterEmployeesWagesSum(DbSet<Expenses> allExpenses, int currentCostCenterId,
            int activeFinancialYearId, CostCenter currentCostCenter)
        {
            var currentCostCenterEmployees = allExpenses
                .Where(c => c.CostCenterId == currentCostCenterId && c.EmployeeId != null &&
                            c.FinancialYearId == activeFinancialYearId);
            currentCostCenter.DirectWagesCost = currentCostCenterEmployees.Sum(a => a.Amount);
            decimal totalDirectCostSum = currentCostCenter.DirectWagesCost;
            return totalDirectCostSum;
        }

        private static void CurrentEmployeeCount(DbSet<Expenses> allExpenses, int currentCostCenterId, int activeFinancialYearId,
            CostCenter currentCostCenter)
        {
            var currentCostCenterEmployees = allExpenses
                .Where(c => c.CostCenterId == currentCostCenterId && c.EmployeeId != null &&
                            c.FinancialYearId == activeFinancialYearId);

            currentCostCenter.DirectAllocatedStuff = currentCostCenterEmployees.Count();
        }

    }
}
