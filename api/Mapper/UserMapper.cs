using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Auth;
using api.Models;

namespace api.Mapper
{
    public static class UserMapper
    {
        public static UserDTO CreateUserDTOfromUser(this ApplicationUser applicationUser)
        {
            return new UserDTO
            {
                PhoneNumber = applicationUser.PhoneNumber,
                CreatedAt = applicationUser.CreatedAt,
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                NormalizedUserName = applicationUser.NormalizedUserName,
                Email = applicationUser.Email
            };
        }
    }
}