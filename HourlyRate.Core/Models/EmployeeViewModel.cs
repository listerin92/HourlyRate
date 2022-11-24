using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
        
        [Required]
        [Display(Name = "Price per month")]
        [Range(0.00, 200000.00, ErrorMessage = "Price per month must be a positive number and less than {2} digits")]
        public decimal Salary { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<EmployeeDepartmentModel> EmployeeDepartments { get; set; }
            = new List<EmployeeDepartmentModel>();
    }
}
