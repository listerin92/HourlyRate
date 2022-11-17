using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HourlyRate.Infrastructure.Models.Account
{
    public class User : IdentityUser
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
        public string VAT { get; set; } = null!;

    }
}
