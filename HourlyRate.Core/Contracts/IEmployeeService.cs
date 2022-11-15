using HourlyRate.Core.Models;

namespace HourlyRate.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> AllEmployees();

    }
}
