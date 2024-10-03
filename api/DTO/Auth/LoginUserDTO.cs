using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Auth
{
    public class LoginUserDTO
    {
        [EmailAddress]
        [Required(ErrorMessage = "UserName or Email is required")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}