using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JWTAuthentication.AppDBContext;

public class AuthService
{
    private readonly UserDBContext _context;

    public AuthService(UserDBContext context)
    {
        _context = context;
    }

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

        return "Login successful";
    }
}
