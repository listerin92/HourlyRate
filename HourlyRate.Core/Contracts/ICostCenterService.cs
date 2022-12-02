using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Models.GeneralCost;

namespace HourlyRate.Core.Contracts
{
    public interface ICostCenterService
    {
        Task<IEnumerable<GeneralCostTypeViewModel>> AllCostTypes();
        Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments();

        Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId);
        Task AddCostCenter(AddCostCenterViewModel ccModel, Guid companyId);
        Task AddCostCenterToEmployee(AddCostCenterViewModel ccModel);

    }
}
