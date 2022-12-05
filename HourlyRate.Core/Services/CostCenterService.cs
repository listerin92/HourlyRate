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
                    IndirectOtherCost = c.IndirectOtherCost,

                }).ToListAsync();
            return await allCostCentersUpdated;
        }

        private async Task UpdateAllCostCenters(Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var allCostCenters = _context.CostCenters
                .Where(c => c.CompanyId == companyId && c.Name != "None"
                                                     && c.FinancialYearId == activeFinancialYearId).ToList();

            foreach (var currentCostCenter in allCostCenters)
            {
                var totalDirectCostSum = 0.0m;
                var currentCostCenterId = currentCostCenter.Id;

                var allExpenses = _context.Expenses;

                //----------- Employees count wages
                CurrentEmployeeCount(allExpenses, currentCostCenterId, activeFinancialYearId, currentCostCenter);

                //---------- EmployeesWages
                totalDirectCostSum += CurrentCostCenterEmployeesWagesSum(allExpenses, currentCostCenterId,
                    activeFinancialYearId, currentCostCenter);

                //----------- Consumables
                totalDirectCostSum += CurrentCostCenterConsumablesTotal(allExpenses, currentCostCenterId,
                    activeFinancialYearId, currentCostCenter);

                //---------- Repair
                //TODO: All 10 predefined CostCategories can be Switched, but will not follow vertical flow of the View
                totalDirectCostSum += CurrentCostCenterDirectRepairSum(allExpenses, currentCostCenterId,
                    activeFinancialYearId, currentCostCenter, 7);

                //----------- Direct Depreciation
                totalDirectCostSum += CurrentCostCenterDepreciationSum(allExpenses, currentCostCenterId,
                    activeFinancialYearId, currentCostCenter, 8);

                //-------------Rent
                var totalRentSpace = TotalRentSpace(allCostCenters);

                var rentCost = RentCostTotal(allExpenses, 6);

                totalDirectCostSum += CurrentCostCenterRent(rentCost, totalRentSpace, currentCostCenter);

                //-----------Electricity
                var totalElectricCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 2);

                currentCostCenter.TotalPowerConsumption =
                    currentCostCenter.AnnualChargeableHours * currentCostCenter.AvgPowerConsumptionKwh;

                var electricityPricePerKwhIndirectlyCalculated =
                    ElectricityPricePerKwhIndirectlyCalculated(totalElectricCost, allCostCenters);

                currentCostCenter.DirectElectricityCost =
                    currentCostCenter.TotalPowerConsumption * electricityPricePerKwhIndirectlyCalculated;

                totalDirectCostSum += currentCostCenter.DirectElectricityCost;

                //---------Heating
                var heatingCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 9);

                var heatingPerSqM = heatingCost / totalRentSpace;
                currentCostCenter.IndirectHeatingCost = currentCostCenter.FloorSpace * heatingPerSqM;

                //--------Total Direct Cost
                totalDirectCostSum += currentCostCenter.FloorSpace * heatingPerSqM;
                currentCostCenter.TotalDirectCosts = totalDirectCostSum;

                //--------Total Index - Total Direct Cost / Current Total Direct cost
                var sumTotalDirectCosts = SumTotalDirectCosts(allCostCenters);
                currentCostCenter.TotalIndex = sumTotalDirectCosts / currentCostCenter.TotalDirectCosts;


                //----WaterIndex - Total Direct Of CC Using Water / Current Total Direct 

                var tDirectCostOfCcUsingWater = allCostCenters.Where(s => s.IsUsingWater == true)
                    .Sum(s => s.TotalDirectCosts);
                var totalWaterCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 1);

                SetWaterCost(currentCostCenter, tDirectCostOfCcUsingWater, totalWaterCost);

                //-------Taxes
                var taxCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 10);
                currentCostCenter.IndirectTaxes = taxCosts / currentCostCenter.TotalIndex;

                // ---- Phones
                var phonesCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 3);
                currentCostCenter.IndirectTaxes = phonesCosts / currentCostCenter.TotalIndex;

                // -----Other
                var otherCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 4);
                currentCostCenter.IndirectOtherCost = otherCosts / currentCostCenter.TotalIndex;

                //------General Administration
                var administrationCost = GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 5);
                currentCostCenter.IndirectAdministrationWagesCost = administrationCost / currentCostCenter.TotalIndex;

                //---------- EmployeesMaintenanceWages
                //var employeesMaintenanceWages= MaintenanceCostCenterEmployeesWagesSum(allExpenses, currentCostCenterId,
                //    activeFinancialYearId, currentCostCenter);

                _context.CostCenters.Update(currentCostCenter);
            }


            await _context.SaveChangesAsync();
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

        private static void SetWaterCost(CostCenter currentCostCenter, decimal tDirectCostOfCcUsingWater,
            decimal totalWaterCost)
        {
            if (currentCostCenter.IsUsingWater)
            {
                currentCostCenter.WaterTotalIndex = tDirectCostOfCcUsingWater / currentCostCenter.TotalDirectCosts;
                currentCostCenter.IndirectWaterCost = totalWaterCost / currentCostCenter.WaterTotalIndex;
            }
            else
            {
                currentCostCenter.WaterTotalIndex = 0;
                currentCostCenter.IndirectWaterCost = 0;
            }
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

        private static decimal MaintenanceCostCenterEmployeesWagesSum(DbSet<Expenses> allExpenses, int currentCostCenterId,
            int activeFinancialYearId, CostCenter currentCostCenter)
        {
            var currentCostCenterEmployees = allExpenses
                .Where(c => c.CostCenterId == currentCostCenterId && c.EmployeeId != null &&
                            c.FinancialYearId == activeFinancialYearId);
            currentCostCenter.DirectWagesCost = currentCostCenterEmployees.Sum(a => a.Amount);
            decimal totalDirectCostSum = currentCostCenter.DirectWagesCost;
            return totalDirectCostSum;
        }

        //private static decimal CurrentCostCenterEmployeesWagesSum(DbSet<Expenses> allExpenses, int currentCostCenterId,
        //    int activeFinancialYearId, CostCenter currentCostCenter)
        //{
        //    var emplMaint = 
        //}

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
