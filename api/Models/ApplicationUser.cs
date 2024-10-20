using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(10)]
        public override string? PhoneNumber { get; set; } = null;
        public DateTime CreatedAt { get; set; }
        public string PreferredLanguage { get; set; }
        public string PreferredCurrency { get; set; }

        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}