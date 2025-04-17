using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerRoleId = "35f570fa-abe6-4d01-bccf-b4807a101ec7";
        var writerRoleId = "dcbaf161-fc3a-43ae-a7aa-2ff7897cc336";

        // Create reader and writer roles
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId,
                Name = "Reader",
                NormalizedName = "READER",
                ConcurrencyStamp = readerRoleId,
            },
            new IdentityRole
            {
                Id = writerRoleId,
                Name = "Writer",
                NormalizedName = "WRITER",
                ConcurrencyStamp = writerRoleId,
            }
        };

        // Seed roles
        builder.Entity<IdentityRole>().HasData(roles);

        // Create admin user
        var adminUserId = "c640a778-59a2-43d9-9b2c-3cd250310f4c";
        var admin = new IdentityUser()
        {
            Id = adminUserId,
            UserName = "admin@codepulse.com",
            Email = "admin@codepulse.com",
            NormalizedEmail = "ADMIN@CODEPULSE.COM",
            NormalizedUserName = "ADMIN@CODEPULSE.COM",
            // Use a static hash value instead of generating one
            PasswordHash = "AQAAAAIAAYagAAAAEFjVq9PX0qkIyioHAw59bugI7p+uDWRcnQdAbW7JtOHYJvbXg0wvt43zo8OBbiGBww==",
            SecurityStamp = "1b3aa451-6fcd-448f-a88d-a08bbb8786c6",
            ConcurrencyStamp = "84bbb0ef-70bd-49bb-b6e4-a9f491401f54"
        };

        builder.Entity<IdentityUser>().HasData(admin);

        // Give roles to admin user
        var adminRoles = new List<IdentityUserRole<string>>()
        {
            new()
            {
                UserId = adminUserId,
                RoleId = readerRoleId
            },
            new()
            {
                UserId = adminUserId,
                RoleId = writerRoleId
            },
        };

        builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
        

    }
}