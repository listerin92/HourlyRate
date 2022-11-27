using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Data.Models;

namespace HourlyRate.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModelCurrency>> AllEmployeesWithSalary(Guid companyId);

        Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments();
        Task<int> CreateEmployee(EmployeeViewModel model, Guid companyId);
        Task CreateExpensesByEmployee(int employeeId, decimal amount, Guid companyId);
        Task<Expenses> GetEmployeeSalary(int employeeId);

        Task<bool> Exists(int id);
        Task Edit(int employeeId, EmployeeViewModel model, Guid companyId);

        Task<int> GetEmployeeDepartmentId(int houseId);
        Task<EmployeeViewModel> EmployeeDetailsById(int id, Guid companyId);
        Task<bool> DepartmentExists(int? departmentId);

        Task Delete(int employeeId);

    }
}
