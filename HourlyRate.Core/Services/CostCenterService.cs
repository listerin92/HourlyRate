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

                }).ToListAsync();
            return await allCostCentersUpdated;
        }

        private async Task UpdateAllCostCenters(Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var allCostCenters = _context.CostCenters
                .Where(c => c.CompanyId == companyId && c.Name != "None").ToList();

            foreach (var currentCostCenter in allCostCenters)
            {
                var totalDirectCostSum = 0.0m;
                var currentCostCenterId = currentCostCenter.Id;

                var allExpenses = _context.Expenses;

                var currentCostCenterEmployees = allExpenses
                    .Where(c => c.CostCenterId == currentCostCenterId && c.EmployeeId != null && c.FinancialYearId == activeFinancialYearId);

                currentCostCenter.DirectAllocatedStuff = currentCostCenterEmployees.Count();
                currentCostCenter.DirectWagesCost = currentCostCenterEmployees.Sum(a => a.Amount);
                totalDirectCostSum += currentCostCenter.DirectWagesCost;

                var currentCostGeneralConsumables = allExpenses
                    .Where(c => c.CostCenterId == currentCostCenterId &&
                                c.ConsumableId != null &&
                                c.FinancialYearId == activeFinancialYearId);

                currentCostCenter.DirectGeneraConsumablesCost = currentCostGeneralConsumables.Sum(c => c.Amount);
                totalDirectCostSum += currentCostCenter.DirectGeneraConsumablesCost;

                var directRepairCost = allExpenses
                    .Where(c => c.CostCenterId == currentCostCenterId && c.CostCategoryId == 7
                    && c.FinancialYearId == activeFinancialYearId)//TODO: Fixed CostCategories 7==Repair
                    .Select(r => r.Amount).Sum();
                currentCostCenter.DirectRepairCost = directRepairCost;
                totalDirectCostSum += directRepairCost;

                var directGeneraDepreciationCost = allExpenses
                    .Where(c => c.CostCenterId == currentCostCenterId && c.CostCategoryId == 8
                                                                      && c.FinancialYearId == activeFinancialYearId)
                    .Select(r => r.Amount).Sum();
                currentCostCenter.DirectDepreciationCost = directGeneraDepreciationCost;
                totalDirectCostSum += directGeneraDepreciationCost;

                var totalElectricCost = allExpenses
                    .Where(c => c.CostCategoryId == 2
                                && c.FinancialYearId == activeFinancialYearId)
                    .Select(r => r.Amount).Sum();



                currentCostCenter.TotalDirectCosts = totalDirectCostSum;

                // ------------End Direct Costs 

                var rentCost = allExpenses
                    .Where(c => c.CostCategoryId == 6)
                    .Select(r => r.Amount).Sum();
                var totalRentSpace = allCostCenters.Select(r => r.FloorSpace).Sum();
                var rentPerSqM = rentCost / totalRentSpace;

                currentCostCenter.RentCost = currentCostCenter.FloorSpace * rentPerSqM;

                var electricityPricePerKwhIndirectlyCalculated = totalElectricCost /
                                                                 allCostCenters.Select(tp => tp.TotalPowerConsumption).Sum();
                currentCostCenter.TotalPowerConsumption = currentCostCenter.AnnualChargeableHours * currentCostCenter.AvgPowerConsumptionKwh;
                currentCostCenter.DirectElectricityCost = currentCostCenter.TotalPowerConsumption * electricityPricePerKwhIndirectlyCalculated;

                var heatingCost = allExpenses
                    .Where(c => c.CostCategoryId == 9)
                    .Select(r => r.Amount).Sum();
                var heatingPerSqM = heatingCost / totalRentSpace;
                currentCostCenter.IndirectHeatingCost = currentCostCenter.FloorSpace * heatingPerSqM;


                _context.CostCenters.Update(currentCostCenter);
            }


            await _context.SaveChangesAsync();
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
                FinancialYearId = activeYearId

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
    }
}
