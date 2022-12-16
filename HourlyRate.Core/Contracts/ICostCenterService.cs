using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Models.GeneralCost;
using HourlyRate.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Core.Contracts
{
    public interface ICostCenterService
    {
        Task<bool> Exists(int id);
        Task<AddCostCenterViewModel> GetCostCenterDetailsById(int id, Guid companyId);
        Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments();

        Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId);
        Task AddCostCenter(AddCostCenterViewModel ccModel, Guid companyId);
        Task AddCostCenterToEmployeeExpenses(AddCostCenterViewModel ccModel);
        Task Edit(int costCenterId, AddCostCenterViewModel model, Guid companyId);
        Task UpdateAllCostCenters(Guid companyId);
        decimal TotalSalaryMaintenanceDepartment(IQueryable<Expenses> allExpenses);

        decimal GetSumOfTotalIndirectCostOfCc(IQueryable<Expenses> allExpenses, int costCategoryId);

        int ActiveFinancialYearId();
        decimal SumTotalDirectMixCosts(List<CostCenter> allCostCenters);

        decimal SetWaterCost(CostCenter currentCostCenter, decimal totalWaterCost);

        decimal CurrentCostCenterRent(decimal rentCost, decimal totalRentSpace, CostCenter currentCostCenter);

        decimal TotalRentSpace(List<CostCenter> allCostCenters);
        decimal ElectricityPricePerKwhIndirectlyCalculated(decimal totalElectricCost, List<CostCenter> allCostCenters);

        decimal CurrentCostCenterDepreciationSum(IQueryable<Expenses> allExpenses, CostCenter currentCostCenter, int costCategoryId);

        decimal RentCostTotal(IQueryable<Expenses> allExpenses, int costCategoryId);

        Task Delete(int costCenterId, Guid companyId);

        decimal CurrentCostCenterDirectRepairSum(IQueryable<Expenses> allExpenses, CostCenter currentCostCenter, int costCategoryId);

        decimal CurrentCostCenterConsumablesTotal(IQueryable<Expenses> allExpenses, CostCenter currentCostCenter);

        decimal CurrentCostCenterEmployeesWagesSum(IQueryable<Expenses> allExpenses, CostCenter currentCostCenter);

        int CurrentEmployeeCount(IQueryable<Expenses> allExpenses, CostCenter currentCostCenter);
    }
}
