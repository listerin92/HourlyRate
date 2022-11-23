using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Infrastructure.Data.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CompanyName { get; set; } = null!;

        public string? CompanyDescription { get; set; }
        public string? CompanyPhone { get; set; } = null!;
        public string? CompanyEmail { get; set; } = null!;
        public string VAT { get; set; } = null!;
    }
}
