using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Game_Universe.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "b15c42ed-2466-434c-adae-d16cc3ec84b4";
            var writerRoleId = "684f8d60-c51a-4a8a-a756-f445eb9c08c9";

            // Create Reader and Writer Role
            var roles = new List<IdentityRole>
    {
        new IdentityRole
        {
            Id = readerRoleId,
            Name = "Reader",
            NormalizedName = "Reader".ToUpper(),
            ConcurrencyStamp = readerRoleId
        },
        new IdentityRole
        {
            Id = writerRoleId,
            Name = "Writer",
            NormalizedName = "Writer".ToUpper(),
            ConcurrencyStamp = writerRoleId
        }
    };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin User
            var adminUserId = "65deb7cb-054b-4b79-a09e-6056e4514f95";
            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@gameuniverse.com",
                Email = "admin@gameuniverse.com",
                NormalizedEmail = "admin@gameuniverse.com".ToUpper(),
                NormalizedUserName = "admin@gameuniverse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Kierpiec@1");
            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles To Admin
            var adminRoles = new List<IdentityUserRole<string>>
    {
        new IdentityUserRole<string>
        {
            UserId = adminUserId,
            RoleId = readerRoleId
        },
        new IdentityUserRole<string>
        {
            UserId = adminUserId,
            RoleId = writerRoleId
        }
    };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
