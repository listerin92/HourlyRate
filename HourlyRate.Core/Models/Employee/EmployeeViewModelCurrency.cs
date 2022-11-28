using HourlyRate.Infrastructure.Data.Models.Employee;

namespace HourlyRate.Core.Models.Employee
{
    public class EmployeeViewModelCurrency : EmployeeViewModel
    {
        public string DefaultCurrency { get; set; } = null!;
        public Department Department { get; set; }

    }
}
