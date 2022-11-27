using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Infrastructure.Data.Models
{
    public class CostCenter
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
