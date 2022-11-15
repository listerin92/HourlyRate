using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Infrastructure.Models.Employee
{
    public class Department
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
