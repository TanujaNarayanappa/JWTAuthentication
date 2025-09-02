using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthSerive authSerive) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task <ActionResult<User>> Register(UserDto userDto)
        {
            //var hashPassword = new PasswordHasher<User>().HashPassword(user, userDto.password);
            //user.username = userDto.username;
            //user.password = hashPassword;
            var user = await authSerive.RegisterAsync(userDto);
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto userDto)
        {
            var result = await authSerive.LoginAsync(userDto);
            if (result == null)
                return BadRequest("Invalid username or password");
            return Ok(result);
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            return Ok("You are authenticated!");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Hello Admin!");
        }
    }

}
