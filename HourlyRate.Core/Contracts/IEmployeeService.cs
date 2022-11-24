using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Data.Models;

namespace HourlyRate.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModelCurrency>> AllEmployeesWithSalary(Guid companyId);

        Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments();
        Task<bool> DepartmentExists(int categoryId);
        Task<int> CreateEmployee(EmployeeViewModel model, Guid companyId);
        Task CreateExpensesByEmployee(int employeeId, EmployeeViewModel employee, Guid companyId);





    }
}
