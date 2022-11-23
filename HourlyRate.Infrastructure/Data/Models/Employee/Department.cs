using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Infrastructure.Data.Models.Employee
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

    }
}
