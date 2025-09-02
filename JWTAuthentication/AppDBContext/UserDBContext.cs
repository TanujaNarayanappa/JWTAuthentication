using JWTAuthentication.Entities;
using Microsoft.EntityFrameworkCore;


namespace JWTAuthentication.AppDBContext
{
    public class UserDBContext : DbContext // Simplified namespace usage
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Corrected DbSet type
    }
}