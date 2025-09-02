using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JWTAuthentication.AppDBContext;
using System.Threading.Tasks;
using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace JWTAuthentication.Services
{
    public class AuthService(IConfiguration configuration,UserDBContext _context) : IAuthSerive
    {
      

        public async Task<User?> RegisterAsync(UserDto userDto)
        {
            var user = new User
            {
                username = userDto.username
            };

            user.password = new PasswordHasher<User>().HashPassword(user, userDto.password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string?> LoginAsync(UserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == userDto.username);
            if (user == null)
                return null;

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.password, userDto.password);
            if (result == PasswordVerificationResult.Failed)
                return null;
            var token = createToken(user);
            return token;
        }
        private string createToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
    issuer: configuration.GetValue<string>("AppSettings:Issuer"),
    audience: configuration.GetValue<string>("AppSettings:Audience"),
    claims: claims,
    expires: DateTime.UtcNow.AddDays(1),
    signingCredentials: creds
);


            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }
    }
}
