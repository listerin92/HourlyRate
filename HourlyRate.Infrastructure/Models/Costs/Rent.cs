using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Infrastructure.Models.Costs
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Company))]
        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; } = null!;

    }
}
