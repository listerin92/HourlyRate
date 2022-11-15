using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models
{
    public class EmployeeViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string JobTitle { get; set; } = null!;

        public decimal Salary { get; set; }
    }
}
