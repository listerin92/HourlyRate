using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HourlyRate.Infrastructure.Models.Account;

namespace HourlyRate.Infrastructure.Models.Employee
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        
        [Required]
        public string JobTitle { get; set; } = null!;

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Required] 
        public bool IsEmployee { get; set; } = true;

        public Department Department { get; set; } = null!;

        public string? UserId { get; set; }


    }
}
