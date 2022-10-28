using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HourlyRate.Core.Models.Account
{
    public class User : IdentityUser
    {
        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }
    }
}
