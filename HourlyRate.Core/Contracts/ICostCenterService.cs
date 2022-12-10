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
        Task AddCostCenterToEmployee(AddCostCenterViewModel ccModel);
        Task Edit(int costCenterId, AddCostCenterViewModel model, Guid companyId);
        Task UpdateAllCostCenters(Guid companyId);
        decimal TotalSalaryMaintenanceDepartment(DbSet<Expenses> allExpenses, int activeFinancialYearId);

        decimal GetSumOfTotalIndirectCostOfCc(DbSet<Expenses> allExpenses, int activeFinancialYearId,
            int costCategoryId);

        int ActiveFinancialYearId();
        decimal SumTotalDirectCosts(List<CostCenter> allCostCenters);

        decimal SetWaterCost(CostCenter currentCostCenter, decimal tDirectCostOfCcUsingWater,
            decimal totalWaterCost);
    }
}
