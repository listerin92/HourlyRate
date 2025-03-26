using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Models.Spektar;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Spektar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace HourlyRate.Core.Services
{
    public class CostCenterService : ICostCenterService
    {
        private readonly ApplicationDbContext _context;
        private readonly SPEKTAR_NEWContext _spektarNewContext;
        private readonly int _currentFinancialYearId;
        private readonly ILogger<CostCenterService> _logger;
        public CostCenterService(
            ApplicationDbContext context,
            SPEKTAR_NEWContext spektarNewContext,
            ILogger<CostCenterService> logger
        )
        {
            _spektarNewContext = spektarNewContext;
            _context = context;
            _logger = logger;
            _currentFinancialYearId = ActiveFinancialYearId();
        }
        public async Task<bool> Exists(int id)
        {
            return await _context.CostCenters
                .AnyAsync(h => h.Id == id);
        }

        public async Task<AddCostCenterViewModel> GetCostCenterDetailsById(int id, Guid companyId)
        {
            return await _context.CostCenters
                .Where(costCenter => costCenter.CompanyId == companyId && costCenter.Id == id)
                .Select(cc => new AddCostCenterViewModel()
                {
                    Id = id,
                    Name = cc.Name,
                    AnnualHours = cc.AnnualHours,
                    AnnualChargeableHours = cc.AnnualChargeableHours,
                    AvgPowerConsumptionKwh = cc.AvgPowerConsumptionKwh,
                    FloorSpace = cc.FloorSpace,
                    IsUsingWater = cc.IsUsingWater,
                    DepartmentId = cc.DepartmentId,
                })
                .FirstAsync();
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

            var getCostCenter = _context.CostCenters
                .FirstOrDefault(cc => cc.Name == ccModel.Name
                                      && cc.FinancialYearId == _currentFinancialYearId
                                      && cc.IsActive == true);

            if (getCostCenter != null)
            {
                throw new ArgumentException("Name already exists");
            }

            var employeeNo = _context.Expenses
                .Where(f => f.FinancialYearId == _currentFinancialYearId)
                .Count(e => e.Employee!.DepartmentId == ccModel.DepartmentId);

            var employeSalary = _context.Expenses
                .Where(e => e.Employee!.Department!.Id == ccModel.DepartmentId
                && e.FinancialYearId == _currentFinancialYearId)
                .Sum(e => e.Amount);

            //TODO: Check if departmentId is already assigned ?? 
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
                FinancialYearId = _currentFinancialYearId,
                IsActive = true,

            };

            await _context.AddAsync(costCenter);
            await _context.SaveChangesAsync();

        }


        /// <summary>
        /// Add Cost Center To Employee Expenses(Salary)
        /// </summary>
        /// <param name="ccModel"></param>
        /// <returns></returns>
        public async Task AddCostCenterToEmployeeExpenses(AddCostCenterViewModel ccModel)
        {

            //Unique name of cost center is checked when is created
            var getCostCenter = _context.CostCenters
                .First(cc => cc.Name == ccModel.Name
                             && cc.FinancialYearId == _currentFinancialYearId
                             && cc.IsActive == true);

            var employeeExpenses = _context.Expenses
                .Where(e => e.Employee!.Department!.Id == getCostCenter.DepartmentId
                && e.FinancialYearId == _currentFinancialYearId
                && e.Employee.IsEmployee == true);

            foreach (var e in employeeExpenses)
            {
                e.CostCenterId = getCostCenter.Id;
            }

            _context.UpdateRange(employeeExpenses);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Edit Cost Center
        /// </summary>
        /// <param name="costCenterId"></param>
        /// <param name="model"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task Edit(int costCenterId, AddCostCenterViewModel model, Guid companyId)
        {
            //TODO: Bug, need to calculated indexes in add otherwise need a second update
            var costCenter = _context.CostCenters
                .Where(costCenter => costCenter.CompanyId == companyId && costCenter.Id == costCenterId)
                .Select(costCenter => new CostCenter()
                {
                    Id = model.Id,
                    Name = model.Name,
                    FloorSpace = model.FloorSpace,
                    AnnualHours = model.AnnualHours,
                    AnnualChargeableHours = model.AnnualChargeableHours,
                    AvgPowerConsumptionKwh = model.AvgPowerConsumptionKwh,
                    DepartmentId = model.DepartmentId,
                    IsUsingWater = model.IsUsingWater,
                    CompanyId = companyId,
                    FinancialYearId = _currentFinancialYearId,
                    IsActive = model.IsActive,
                }).First();

            _context.CostCenters.Update(costCenter);
            await _context.SaveChangesAsync();



        }

        /// <summary>
        /// Get Active Financial Year Id
        /// </summary>
        /// <returns></returns>
        public int ActiveFinancialYearId()
        {
            return _context.FinancialYears
                .First(y => y.IsActive).Id;
        }

        /// <summary>
        /// Show All CostCenters
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId)
        {
            var result = CostPerCostCategoryPerPeriod();

            var defaultCurrency = _context.Companies
                .First(c => c.Id == companyId).DefaultCurrency;


            var allCostCentersUpdated = _context.CostCenters
                .Where(c => c.CompanyId == companyId && c.Name != "None"
                                                 && c.FinancialYearId == _currentFinancialYearId
                                                 && c.IsActive == true)
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

        /// <summary>
        /// Update/Recalculate All CostCenter
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task UpdateAllCostCenters(Guid companyId)
        {
            var allCostCenters = _context.CostCenters
                .Where(c => c.CompanyId == companyId &&
                            c.Name != "None" &&
                            c.FinancialYearId == _currentFinancialYearId &&
                            c.IsActive == true).ToList();

            var allExpenses = _context.Expenses
                .Where(e => e.FinancialYearId == _currentFinancialYearId && e.IsDeleted == false);

            var totalRentSpace = TotalRentSpace(allCostCenters);
            var rentCost = SumPerCostCategoryForAllCostCenters(allExpenses, 6);
            var totalElectricCost = GetSumOfTotalIndirectCostOfCc(allExpenses, 2);
            var electricityPricePerKwhIndirectlyCalculated = ElectricityPricePerKwhIndirectlyCalculated(totalElectricCost, allCostCenters);
            var heatingCost = GetSumOfTotalIndirectCostOfCc(allExpenses, 9);
            var heatingPerSqM = heatingCost / totalRentSpace;
            var totalWaterCost = GetSumOfTotalIndirectCostOfCc(allExpenses, 1);
            var taxCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, 10);
            var phonesCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, 3);
            var otherCosts = GetSumOfTotalIndirectCostOfCc(allExpenses, 4);
            var administrationCost = GetSumOfTotalIndirectCostOfCc(allExpenses, 5);
            var totalSalaryMaintenance = TotalSalaryMaintenanceDepartment(allExpenses);
            var indirectDepreciationCost = GetSumOfTotalIndirectCostOfCc(allExpenses, 11);

            foreach (var costCenter in allCostCenters)
            {
                var totalDirectCostSum = 0.0m;
                var totalMixCostSum = 0.0m;
                var totalIndirectCostSum = 0.0m;

                // Log initial state
                _logger.LogInformation("Updating cost center: {CostCenterId}", costCenter.Id);

                // Employees count wages
                costCenter.DirectAllocatedStuff = CurrentEmployeeCount(allExpenses, costCenter);
                _logger.LogInformation("DirectAllocatedStuff: {DirectAllocatedStuff}", costCenter.DirectAllocatedStuff);

                // EmployeesWages
                totalDirectCostSum += CurrentCostCenterEmployeesWagesSum(allExpenses, costCenter);
                _logger.LogInformation("TotalDirectCostSum after EmployeesWages: {TotalDirectCostSum}", totalDirectCostSum);

                // Repair
                totalDirectCostSum += CurrentCostCenterDirectRepairSum(allExpenses, costCenter, 7);
                _logger.LogInformation("TotalDirectCostSum after Repair: {TotalDirectCostSum}", totalDirectCostSum);

                // Consumables
                totalDirectCostSum += CurrentCostCenterConsumablesTotal(allExpenses, costCenter);
                _logger.LogInformation("TotalDirectCostSum after Consumables: {TotalDirectCostSum}", totalDirectCostSum);

                // Direct Depreciation
                totalDirectCostSum += CurrentCostCenterDepreciationSum(allExpenses, costCenter, 8);
                _logger.LogInformation("TotalDirectCostSum after Depreciation: {TotalDirectCostSum}", totalDirectCostSum);

                // Total Direct Cost
                costCenter.TotalDirectCosts = totalDirectCostSum;
                _logger.LogInformation("TotalDirectCosts: {TotalDirectCosts}", costCenter.TotalDirectCosts);

                // Rent
                totalMixCostSum += CurrentCostCenterRent(rentCost, totalRentSpace, costCenter);
                _logger.LogInformation("TotalMixCostSum after Rent: {TotalMixCostSum}", totalMixCostSum);

                // Electricity
                costCenter.TotalPowerConsumption = costCenter.AnnualChargeableHours * costCenter.AvgPowerConsumptionKwh;
                costCenter.DirectElectricityCost = costCenter.TotalPowerConsumption * electricityPricePerKwhIndirectlyCalculated;
                totalMixCostSum += costCenter.DirectElectricityCost;
                _logger.LogInformation("TotalMixCostSum after Electricity: {TotalMixCostSum}", totalMixCostSum);

                // Heating
                costCenter.IndirectHeatingCost = costCenter.FloorSpace * heatingPerSqM;
                totalMixCostSum += costCenter.FloorSpace * heatingPerSqM;
                _logger.LogInformation("TotalMixCostSum after Heating: {TotalMixCostSum}", totalMixCostSum);

                // Total Mix Cost
                costCenter.TotalMixCosts = totalMixCostSum + totalDirectCostSum;
                _logger.LogInformation("TotalMixCosts: {TotalMixCosts}", costCenter.TotalMixCosts);

                // Total Index
                var sumTotalDirectMixCosts = SumTotalDirectMixCosts(allCostCenters);
                costCenter.TotalIndex = costCenter.TotalMixCosts != 0 ? sumTotalDirectMixCosts / costCenter.TotalMixCosts : 0;
                _logger.LogInformation("TotalIndex: {TotalIndex}", costCenter.TotalIndex);

                // Water Index and Cost
                var tDirectMixCostOfCcUsingWater = allCostCenters.Where(s => s.IsUsingWater).Sum(s => s.TotalMixCosts);
                costCenter.WaterTotalIndex = costCenter.TotalMixCosts != 0 ? tDirectMixCostOfCcUsingWater / costCenter.TotalMixCosts : 0;
                totalIndirectCostSum += SetWaterCost(costCenter, totalWaterCost);
                _logger.LogInformation("TotalIndirectCostSum after Water Cost: {TotalIndirectCostSum}", totalIndirectCostSum);

                // Taxes
                costCenter.IndirectTaxes = costCenter.TotalIndex != 0 ? taxCosts / costCenter.TotalIndex : 0;
                totalIndirectCostSum += costCenter.IndirectTaxes;
                _logger.LogInformation("TotalIndirectCostSum after Taxes: {TotalIndirectCostSum}", totalIndirectCostSum);

                // Phones
                costCenter.IndirectPhonesCost = costCenter.TotalIndex != 0 ? phonesCosts / costCenter.TotalIndex : 0;
                totalIndirectCostSum += costCenter.IndirectPhonesCost;
                _logger.LogInformation("TotalIndirectCostSum after Phones: {TotalIndirectCostSum}", totalIndirectCostSum);

                // Other Costs
                costCenter.IndirectOtherCost = costCenter.TotalIndex != 0 ? otherCosts / costCenter.TotalIndex : 0;
                totalIndirectCostSum += costCenter.IndirectOtherCost;
                _logger.LogInformation("TotalIndirectCostSum after Other Costs: {TotalIndirectCostSum}", totalIndirectCostSum);

                // General Administration
                costCenter.IndirectAdministrationWagesCost = costCenter.TotalIndex != 0 ? administrationCost / costCenter.TotalIndex : 0;
                totalIndirectCostSum += costCenter.IndirectAdministrationWagesCost;
                _logger.LogInformation("TotalIndirectCostSum after Administration: {TotalIndirectCostSum}", totalIndirectCostSum);

                // Maintenance Wages
                costCenter.IndirectMaintenanceWagesCost = costCenter.TotalIndex != 0 ? totalSalaryMaintenance / costCenter.TotalIndex : 0;
                totalIndirectCostSum += costCenter.IndirectMaintenanceWagesCost;
                _logger.LogInformation("TotalIndirectCostSum after Maintenance Wages: {TotalIndirectCostSum}", totalIndirectCostSum);

                // Indirect Depreciation
                costCenter.IndirectDepreciationCost = costCenter.TotalIndex != 0 ? indirectDepreciationCost / costCenter.TotalIndex : 0;
                totalIndirectCostSum += costCenter.IndirectDepreciationCost;
                _logger.LogInformation("TotalIndirectCostSum after Indirect Depreciation: {TotalIndirectCostSum}", totalIndirectCostSum);

                // Total Costs
                costCenter.IndirectTotalCosts = totalIndirectCostSum;
                costCenter.TotalCosts = costCenter.TotalMixCosts + costCenter.IndirectTotalCosts;
                _logger.LogInformation("TotalCosts: {TotalCosts}", costCenter.TotalCosts);

                // Wages per Month
                costCenter.WagesPerMonth = (costCenter.DirectWagesCost + costCenter.IndirectAdministrationWagesCost + costCenter.IndirectMaintenanceWagesCost) / 12;
                _logger.LogInformation("WagesPerMonth: {WagesPerMonth}", costCenter.WagesPerMonth);

                // Machine per Month
                costCenter.MachinesPerMonth = (costCenter.DirectRepairCost + costCenter.DirectGeneraConsumablesCost + costCenter.DirectDepreciationCost + costCenter.IndirectDepreciationCost) / 12;
                _logger.LogInformation("MachinesPerMonth: {MachinesPerMonth}", costCenter.MachinesPerMonth);

                // Overheads per Month
                costCenter.OverheadsPerMonth = (costCenter.DirectGeneraConsumablesCost + costCenter.RentCost + costCenter.DirectElectricityCost + costCenter.IndirectHeatingCost + costCenter.IndirectWaterCost + costCenter.IndirectTaxes + costCenter.IndirectPhonesCost + costCenter.IndirectOtherCost) / 12;
                _logger.LogInformation("OverheadsPerMonth: {OverheadsPerMonth}", costCenter.OverheadsPerMonth);

                // Wages per Hour
                costCenter.WagesPerHour = (costCenter.DirectWagesCost + costCenter.IndirectAdministrationWagesCost + costCenter.IndirectMaintenanceWagesCost) / costCenter.AnnualChargeableHours;
                _logger.LogInformation("WagesPerHour: {WagesPerHour}", costCenter.WagesPerHour);

                // Machine per Hour
                costCenter.MachinesPerHour = (costCenter.DirectRepairCost + costCenter.DirectGeneraConsumablesCost + costCenter.DirectDepreciationCost + costCenter.IndirectDepreciationCost) / costCenter.AnnualChargeableHours;
                _logger.LogInformation("MachinesPerHour: {MachinesPerHour}", costCenter.MachinesPerHour);

                // Overheads per Hour
                costCenter.OverheadsPerHour = (costCenter.DirectGeneraConsumablesCost + costCenter.RentCost + costCenter.DirectElectricityCost + costCenter.IndirectHeatingCost + costCenter.IndirectWaterCost + costCenter.IndirectTaxes + costCenter.IndirectPhonesCost + costCenter.IndirectOtherCost) / costCenter.AnnualChargeableHours;
                _logger.LogInformation("OverheadsPerHour: {OverheadsPerHour}", costCenter.OverheadsPerHour);

                // Total Hourly Cost Rate
                costCenter.TotalHourlyCostRate = costCenter.WagesPerHour + costCenter.MachinesPerHour + costCenter.OverheadsPerHour;
                _logger.LogInformation("TotalHourlyCostRate: {TotalHourlyCostRate}", costCenter.TotalHourlyCostRate);

                _context.CostCenters.Update(costCenter);
            }
            await _context.SaveChangesAsync();
        }

        public decimal TotalSalaryMaintenanceDepartment(IQueryable<Expenses> allExpenses)
        {
            var employeesFromMaintenanceDept = allExpenses
                .Where(e => e.Employee!.Department.Name == "Maintenance").ToList();

            return employeesFromMaintenanceDept.Sum(s => s.Amount);
        }

        public decimal GetSumOfTotalIndirectCostOfCc(IQueryable<Expenses> allExpenses, int costCategoryId)
        {
            var totalIndirectCost = allExpenses
                .Where(c => c.CostCategoryId == costCategoryId
                            && c.IsDeleted == false)
                .Select(r => r.Amount).Sum();
            return totalIndirectCost;
        }

        public decimal SumTotalDirectMixCosts(List<CostCenter> allCostCenters)
        {
            var sumTotalDirectCosts = allCostCenters.Sum(s => s.TotalMixCosts);
            return sumTotalDirectCosts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentCostCenter"></param>
        /// <param name="totalWaterCost"></param>
        /// <returns></returns>
        public decimal SetWaterCost(CostCenter currentCostCenter, decimal totalWaterCost)
        {
            if (currentCostCenter.IsUsingWater)
            {
                try
                {
                    return currentCostCenter.IndirectWaterCost =
                        totalWaterCost / currentCostCenter.WaterTotalIndex;
                }
                catch (DivideByZeroException)
                {

                    currentCostCenter.WaterTotalIndex = 0;
                    return currentCostCenter.IndirectWaterCost = 0;
                }

            }

            currentCostCenter.WaterTotalIndex = 0;
            return currentCostCenter.IndirectWaterCost = 0;
        }

        /// <summary>
        /// Electricity Cost Per Kw hour
        /// </summary>
        /// <param name="totalElectricCost"></param>
        /// <param name="allCostCenters"></param>
        /// <returns>Return Indirectly Calculated Electricity cost of Given Cost Centers</returns>
        public decimal ElectricityPricePerKwhIndirectlyCalculated(decimal totalElectricCost, List<CostCenter> allCostCenters)
        {
            var electricityPricePerKwhIndirectlyCalculated = totalElectricCost /
                                                             allCostCenters.Select(tp => tp.TotalPowerConsumption).Sum();
            return electricityPricePerKwhIndirectlyCalculated;
        }

        /// <summary>
        /// Current Cost Center Rent
        /// </summary>
        /// <param name="rentCost"></param>
        /// <param name="totalRentSpace"></param>
        /// <param name="currentCostCenter"></param>
        /// <returns>Return Rent Cost of Current Cost Center</returns>
        public decimal CurrentCostCenterRent(decimal rentCost, decimal totalRentSpace, CostCenter currentCostCenter)
        {

            var rentPerSqM = rentCost / totalRentSpace;

            return currentCostCenter.RentCost = currentCostCenter.FloorSpace * rentPerSqM;
        }

        /// <summary>
        /// Calculate Sum per CostCategory for All Cost Centers
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="costCategoryId"></param>
        /// <returns>Return Total Sum</returns>
        public decimal SumPerCostCategoryForAllCostCenters(IQueryable<Expenses> allExpenses, int costCategoryId)
        {
            var result = allExpenses
                .Where(c => c.CostCategoryId == costCategoryId)
                .Select(r => r.Amount).Sum();
            return result;
        }

        /// <summary>
        /// Delete CostCenter By Id
        /// </summary>
        /// <param name="costCenterId"></param>
        /// <param name="companyId"></param>
        /// <returns>Return Task</returns>
        public async Task Delete(int costCenterId, Guid companyId)
        {
            var currentCostCenter = _context.CostCenters.First(cc => cc.Id == costCenterId && cc.CompanyId == companyId);
            currentCostCenter.IsActive = false;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// All CostCenter Rented Space in m2
        /// </summary>
        /// <param name="allCostCenters"></param>
        /// <returns>Return Rented Space in m2</returns>
        public decimal TotalRentSpace(List<CostCenter> allCostCenters)
        {
            var totalRentSpace = allCostCenters.Select(r => r.FloorSpace).Sum();
            return totalRentSpace;
        }

        /// <summary>
        /// Calculate CurrentCostCenter Direct Depreciation Sum
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenter"></param>
        /// <param name="costCategoryId"></param>
        /// <returns>Return CurrentCostCenter Direct Depreciation Sum</returns>
        public decimal CurrentCostCenterDepreciationSum(IQueryable<Expenses> allExpenses,
             CostCenter currentCostCenter, int costCategoryId)
        {

            var directGeneraDepreciationCost = allExpenses
                .Where(c => c.CostCenterId == currentCostCenter.Id
                            && c.CostCategoryId == costCategoryId
                            && c.IsDeleted == false)
                .Select(r => r.Amount).Sum();

            currentCostCenter.DirectDepreciationCost = directGeneraDepreciationCost;
            decimal totalDirectCostSum = directGeneraDepreciationCost;

            return totalDirectCostSum;
        }

        /// <summary>
        /// Calculate All Cost of Repairs Directly assign to Current Cost Center
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenter"></param>
        /// <param name="costCategoryId"></param>
        /// <returns>Return Sum</returns>
        public decimal CurrentCostCenterDirectRepairSum(IQueryable<Expenses> allExpenses,
            CostCenter currentCostCenter, int costCategoryId)
        {
            var directRepairCost = allExpenses
                .Where(c => c.CostCenterId == currentCostCenter.Id
                                 && c.CostCategoryId == costCategoryId
                                 && c.IsDeleted == false)
                .Select(r => r.Amount).Sum();
            currentCostCenter.DirectRepairCost = directRepairCost;
            return directRepairCost;

        }

        /// <summary>
        /// Calculate All Cost of Consumables assign to Current Cost Center
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenter"></param>
        /// <returns>Return Sum</returns>
        public decimal CurrentCostCenterConsumablesTotal(IQueryable<Expenses> allExpenses,
             CostCenter currentCostCenter)
        {
            var currentCostGeneralConsumables = allExpenses
                .Where(c => c.CostCenterId == currentCostCenter.Id &&
                            c.ConsumableId != null &&
                            c.IsDeleted == false);

            currentCostCenter.DirectGeneraConsumablesCost = currentCostGeneralConsumables.Sum(c => c.Amount);
            decimal totalDirectCostSum = currentCostCenter.DirectGeneraConsumablesCost;
            return totalDirectCostSum;
        }

        /// <summary>
        /// Calculate CurrentCostCenter Employee Wages
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenter"></param>
        /// <returns>Return CurrentCostCenter Total Wages</returns>
        public decimal CurrentCostCenterEmployeesWagesSum(IQueryable<Expenses> allExpenses,
            CostCenter currentCostCenter)
        {
            var currentCostCenterEmployees = allExpenses
                .Where(c => c.CostCenterId == currentCostCenter.Id
                            && c.EmployeeId != null
                            && c.Employee!.IsEmployee == true);
            currentCostCenter.DirectWagesCost = currentCostCenterEmployees.Sum(a => a.Amount);
            var totalDirectCostSum = currentCostCenter.DirectWagesCost;
            return totalDirectCostSum;
        }

        /// <summary>
        /// CountA
        /// </summary>
        /// <param name="allExpenses"></param>
        /// <param name="currentCostCenter"></param>
        /// <returns></returns>
        public int CurrentEmployeeCount(IQueryable<Expenses> allExpenses,
            CostCenter currentCostCenter)
        {
            var currentCostCenterEmployees = allExpenses
                .Where(c => c.CostCenterId == currentCostCenter.Id
                            && c.EmployeeId != null
                            && c.Employee!.IsEmployee == true);

            return currentCostCenterEmployees.Count();
        }

        /// <summary>
        /// Get Costs Of All CostCategories for a given Period 
        /// </summary>
        /// <returns></returns>
        public List<SpektarCostCategoryViewModel> CostPerCostCategoryPerPeriod()
        {
            var result = _spektarNewContext
                .ordrub__
                .AsNoTracking()
                .Where(o => o.order___.dat_open >= DateTime.ParseExact("2022-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            && o.order___.dat_open <= DateTime.ParseExact("2022-12-31", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            && o.order___.open____ == "N"
                                //&&(o.lonen___ != 0 || o.machines != 0 || o.overhead != 0)
                                )
                .GroupBy(g => new { g.rubrik__.rbk__ref, g.rubrik__.oms_rbk_ })
                .Select(g => new SpektarCostCategoryViewModel
                {
                    CostCategoryId = g.Key.rbk__ref,
                    CostCategory = g.Key.oms_rbk_,
                    Wages = g.Sum(s => s.lonen___),
                    Machines = g.Sum(s => s.machines),
                    Overhead = g.Sum(s => s.overhead),
                })
                .OrderBy(o => o.CostCategoryId)
                .ToList();

            return result;
        }
    }
}
