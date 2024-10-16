using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTO.Auth;
using api.Interface;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using stockapi.Extensions;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService tokenService;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check if user with this email /username / phone number already exists in the database
            var userExisted = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == registerUserDTO.Email || user.UserName == registerUserDTO.UserName || user.PhoneNumber == registerUserDTO.PhoneNumber);
            if (userExisted != null)
            {
                return BadRequest("User already exists , Please Login");
            }

            //if above false , create a new user and add it to database

            var newUser = new ApplicationUser
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
                PhoneNumber = registerUserDTO.PhoneNumber
            };

            //create a user to the application
            var CreatedUser = await _userManager.CreateAsync(newUser, registerUserDTO.Password);


            //give success response [ frontend : navigate the user to the login page]
            if (!CreatedUser.Succeeded)
            {
                return StatusCode(500, CreatedUser.Errors);
            }

            return Ok("User Created Successfully, Please Login");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if user with that email or username exists
            var user = await _userManager.FindByEmailAsync(loginUserDto.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginUserDto.UserNameOrEmail);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
            }
            //if exists then validate password and send a token back to the user
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest("You have entered an invalid username or password");
            }

            //TODO: generate token steps
            var token = tokenService.CreateToken(user);

            //send back response and token
            return Ok(new
            {
                token,
                user = user.CreateUserDTOfromUser()
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();
            return Ok("User Logged out Successfully");
        }

        [Authorize]
        [HttpGet("userinfo")]
        public IActionResult GetUserInfo()
        {
            //dummy route to get the user from name
            var username = User.GetUserName();
            return Ok(new { Username = username });
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //get the user from the claims
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Console.WriteLine($"{updateUserDTO.Email} {updateUserDTO.PhoneNumber} {updateUserDTO.UserName}");
            bool IsTokenRefreshed = false;
            bool IsPasswordOrEmailUpdated = false;
            //update the user details
            if (updateUserDTO.Email != null && updateUserDTO.Email != "")
            {
                //check if user already exists with this email
                var userExisted = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == updateUserDTO.Email);
                if (userExisted != null)
                {
                    return BadRequest("User with this email already exists");
                }
                user.Email = updateUserDTO.Email;
                IsTokenRefreshed = true;
                IsPasswordOrEmailUpdated = true;
            }
            if (updateUserDTO.PhoneNumber != null && updateUserDTO.PhoneNumber != "") user.PhoneNumber = updateUserDTO.PhoneNumber;
            if (updateUserDTO.UserName != null && updateUserDTO.UserName != "")
            {
                IsTokenRefreshed = true;
                //check if userNAmes already exists
                var userExisted = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == updateUserDTO.UserName);
                if (userExisted != null)
                {
                    return BadRequest("User with this username already exists");
                }
                user.UserName = updateUserDTO.UserName;
            }


            //password updates
            if (updateUserDTO.OldPassWord != null && updateUserDTO.OldPassWord != "" && updateUserDTO.NewPassWord != null && updateUserDTO.NewPassWord != "")
            {
                IsTokenRefreshed = true;
                IsPasswordOrEmailUpdated = true;
                var userUPDATED = await _userManager.ChangePasswordAsync(user, updateUserDTO.OldPassWord, updateUserDTO.NewPassWord);
                if (!userUPDATED.Succeeded)
                {
                    return StatusCode(500, userUPDATED.Errors);
                }
            }

            //save changes
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                message = "User Updated Successfully",
                token = IsTokenRefreshed ? tokenService.CreateToken(user) : null,
                reAuthorize = IsPasswordOrEmailUpdated,
                user = user.CreateUserDTOfromUser()
            });
        }
    }
}