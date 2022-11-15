using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models.Employee
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; } = null!;

    }
}
