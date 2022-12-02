namespace HourlyRate.Core.Models.Employee
{
    public class AddEmployeeDepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmployeeDepartmentModel> EmployeeDepartments { get; set; }
        = new List<EmployeeDepartmentModel>();
    }
}
