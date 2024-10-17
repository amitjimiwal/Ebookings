using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Auth
{
    public class UpdateUserDTO
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;

        public string? OldPassWord { get; set; } = string.Empty;
        public string? NewPassWord { get; set; } = string.Empty;

        public string? PreferredLanguage { get; set; } = null;
        public string? PreferredCurrency { get; set; } = null;
    }
}