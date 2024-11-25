using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seeding Roles Using EF Core (User, Admin, SuperAdmin)
            var AdminRoleId = "033b2f21-e4ce-4d68-83f8-86a17938a2c5";
            var SuperAdminRoleId = "ce1c6059-e568-44c9-9b8b-d4e54f808bcb";
            var UserRoleId = "99f65dc2-c1ab-4562-ab72-3c499b2beda6";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = SuperAdminRoleId,
                    ConcurrencyStamp = SuperAdminRoleId
                },   
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = UserRoleId,
                    ConcurrencyStamp = UserRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdminUser

            //Creating a User
            var superadminuserId = "b85156e2-5365-41c3-ad0e-22414d7251a4";

            var superAdminUser = new IdentityUser()
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superadminuserId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All the roles to the SuperAdminUser

            var superAdminUserRoles = new List<IdentityUserRole<string>>()
            {
                //Giving all the three roles to the superadmin user
                new IdentityUserRole<string>
                {
                    RoleId = AdminRoleId, 
                    UserId = superadminuserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = SuperAdminRoleId,
                    UserId = superadminuserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = UserRoleId,
                    UserId = superadminuserId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminUserRoles);
        }

    }
}
