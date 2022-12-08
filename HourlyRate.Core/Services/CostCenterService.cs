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
        private readonly ApplicationDbContext _context;

        public CostCenterService(
            ApplicationDbContext context
        )
        {
            _context = context;
        }


        public async Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments()
        {
            return await _context.Departments
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



            var employeeNo = _context.Employees
                .Count(e => e.DepartmentId == ccModel.DepartmentId);

            var employeSalary = _context.Expenses
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

            await _context.AddAsync(costCenter);
            await _context.SaveChangesAsync();


        }

        public async Task AddCostCenterToEmployee(AddCostCenterViewModel ccModel)
        {
            var getCostCenter = _context.CostCenters
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
            return _context.FinancialYears
                .First(y => y.IsActive).Year;
        }
        private int ActiveFinancialYearId()
        {
            return _context.FinancialYears
                .First(y => y.IsActive).Id;
        }

        public async Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId)
        {
            await UpdateAllCostCenters(companyId);

            var defaultCurrency = _context.Companies
                .First(c => c.Id == companyId).DefaultCurrency;


            var allCostCentersUpdated = _context.CostCenters
                .Where(c => c.CompanyId == companyId && c.Name != "None")
                .Select(c => new CostCenterViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    DefaultCurrency = defaultCurrency!,
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
                    TotalDirectCosts = c.TotalDirectCosts,
                    DirectElectricityCost = c.DirectElectricityCost,
                    RentCost = c.RentCost,
                    TotalMixCosts = c.TotalMixCosts,
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

        public async Task UpdateAllCostCenters(Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var allCostCenters = _context.CostCenters
                .Where(c =>
                            c.CompanyId == companyId &&
                            c.Name != "None" &&
                            c.FinancialYearId == activeFinancialYearId).ToList();

            var allExpenses = _context.Expenses;
            var totalSalaryMaintenance = TotalSalaryMaintenanceDepartment(allExpenses, activeFinancialYearId);

            foreach (var costCenter in allCostCenters)
            {
                var totalDirectCostSum = 0.0m;
                var totalMixCostSum = 0.0m;
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

                //TODO: need to change it exactly like the original business logic !!!!!!!!!!
                //--------Total Direct Cost
                costCenter.TotalDirectCosts = totalDirectCostSum;

                //-------------Rent
                var totalRentSpace = TotalRentSpace(allCostCenters);

                var rentCost = RentCostTotal(allExpenses, 6);

                totalMixCostSum += CurrentCostCenterRent(rentCost, totalRentSpace, costCenter);

                //-----------Electricity
                var totalElectricCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 2);

                costCenter.TotalPowerConsumption =
                    costCenter.AnnualChargeableHours * costCenter.AvgPowerConsumptionKwh;

                var totalPowerConsumption =
                    ElectricityPricePerKwhIndirectlyCalculated(totalElectricCost, allCostCenters);

                costCenter.DirectElectricityCost =
                    costCenter.TotalPowerConsumption * totalPowerConsumption;

                totalMixCostSum += costCenter.DirectElectricityCost;

                //---------Heating
                var heatingCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 9);

                var heatingPerSqM = heatingCost / totalRentSpace;
                costCenter.IndirectHeatingCost = costCenter.FloorSpace * heatingPerSqM;
                totalMixCostSum += costCenter.FloorSpace * heatingPerSqM;
                costCenter.TotalMixCosts = totalMixCostSum;

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
                totalIndirectCostSum += costCenter.IndirectTaxes;

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
                     costCenter.IndirectMaintenanceWagesCost) /
                    costCenter.AnnualChargeableHours;

                //--------Machine per Hour
                costCenter.MachinesPerHour =
                    (costCenter.DirectRepairCost +
                     costCenter.DirectGeneraConsumablesCost +
                     costCenter.DirectDepreciationCost +
                     costCenter.IndirectDepreciationCost) /
                    costCenter.AnnualChargeableHours;

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
                     costCenter.IndirectOtherCost) /
                    costCenter.AnnualChargeableHours;

                costCenter.TotalHourlyCostRate =
                    costCenter.WagesPerHour +
                    costCenter.MachinesPerHour +
                    costCenter.OverheadsPerHour;

                _context.CostCenters.Update(costCenter);
            }


            await _context.SaveChangesAsync();
        }

        public decimal TotalSalaryMaintenanceDepartment(DbSet<Expenses> allExpenses, int activeFinancialYearId)
        {
            var employeesFromMaintenanceDept = allExpenses
                .Where(e => e.Employee!.Department.Name == "Maintenance"
                            && e.FinancialYearId == activeFinancialYearId).ToList();

            return employeesFromMaintenanceDept.Sum(s => s.Amount);
        }

        public static decimal GetSumOfTotalIndirectCostOfCc(DbSet<Expenses> allExpenses, int activeFinancialYearId, int costCategoryId)
        {
            var totalIndirectCost = allExpenses
                .Where(c => c.CostCategoryId == costCategoryId
                            && c.FinancialYearId == activeFinancialYearId)
                .Select(r => r.Amount).Sum();
            return totalIndirectCost;
        }
        public static decimal SumTotalDirectCosts(List<CostCenter> allCostCenters)
        {
            var sumTotalDirectCosts = allCostCenters.Sum(s => s.TotalDirectCosts);
            return sumTotalDirectCosts;
        }

        public static decimal SetWaterCost(CostCenter currentCostCenter, decimal tDirectCostOfCcUsingWater,
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

        public static decimal ElectricityPricePerKwhIndirectlyCalculated(decimal totalElectricCost, List<CostCenter> allCostCenters)
        {
            var electricityPricePerKwhIndirectlyCalculated = totalElectricCost /
                                                             allCostCenters.Select(tp => tp.TotalPowerConsumption).Sum();
            return electricityPricePerKwhIndirectlyCalculated;
        }


        public static decimal CurrentCostCenterRent(decimal rentCost, decimal totalRentSpace, CostCenter currentCostCenter)
        {

            var rentPerSqM = rentCost / totalRentSpace;

            return currentCostCenter.RentCost = currentCostCenter.FloorSpace * rentPerSqM;
        }

        public static decimal RentCostTotal(DbSet<Expenses> allExpenses, int costCategoryId)
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
                .Where(c => c.CostCenterId == currentCostCenterId
                            && c.CostCategoryId == costCategoryId
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
                .Where(c => c.CostCenterId == currentCostCenterId
                                 && c.CostCategoryId == costCategoryId
                                 && c.FinancialYearId == activeFinancialYearId)
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
