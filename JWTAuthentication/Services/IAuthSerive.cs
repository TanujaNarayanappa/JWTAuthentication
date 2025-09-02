using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using System.Threading.Tasks;

namespace JWTAuthentication.Services
{
    public interface IAuthSerive
    {
        Task<User?> RegisterAsync(UserDto userDto);
        Task<string?> LoginAsync(UserDto userDto);
    }
}
