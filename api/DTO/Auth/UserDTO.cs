using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Auth
{
    public class UserDTO
    {
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string PreferredLanguage { get; set; } = "EN";
        public string PreferredCurrency { get; set; } = "INR";
        public string ProfilePictureUrl { get; set; }
    }
}