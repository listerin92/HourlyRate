using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Infrastructure.Models.Costs
{
    public class Machine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UniqueId { get; set; }

        [Required] 
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; } = null!;


    }
}
