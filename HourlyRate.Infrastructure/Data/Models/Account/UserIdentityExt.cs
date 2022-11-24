using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Infrastructure.Data.Models.Account
{
    public class UserIdentityExt : IdentityUser
    {
        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        [Required]
        public string CompanyName { get; set; } = null!;

        public string? CompanyDescription { get; set; }

        [Required]
        public string CompanyEmail { get; set; } = null!;

        [Required]
        public string CompanyPhoneNumber { get; set; } = null!;

        [Required]
        public string DefaultCurrency { get; set; } = null!;


        [Required]
        public string VAT { get; set; } = null!;

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;

    }
}
