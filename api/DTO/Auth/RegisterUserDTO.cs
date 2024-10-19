using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Auth
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Preferred Language is required")]
        public string PreferredLanguage { get; set; } = "EN";

        [Required(ErrorMessage = "Preferred Currency is required")]
        public string PreferredCurrency { get; set; } = "INR";
    }
}