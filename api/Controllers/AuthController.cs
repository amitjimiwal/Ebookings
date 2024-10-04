using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Auth;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                user
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();
            return Ok("User Logged out Successfully");
        }
    }
}