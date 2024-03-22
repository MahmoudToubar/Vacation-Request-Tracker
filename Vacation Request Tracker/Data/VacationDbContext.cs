using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vacation_Request_Tracker.Models;

namespace Vacation_Request_Tracker.Data
{
    public class VacationDbContext : IdentityDbContext
    {
        public VacationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TbVacationRequest> VacationsRequest { get; set;}




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin)

            var adminRoleId = "6601fe61-af1e-4e2b-a5d9-1b6b587d9705";
            var userRoleId = "b68f3b1b-fe06-4121-aefb-416ddcfb42ea";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },


                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed Admin

            var AdminId = "a02df147-569e-4cad-b629-a44c8c11091c";
            var AdminUser = new IdentityUser
            {
                UserName = "admin@employee.com",
                Email = "admin@employee.com",
                NormalizedEmail = "admin@employee.com".ToUpper(),
                NormalizedUserName = "admin@employee.com".ToUpper(),
                Id = AdminId
            };

            AdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(AdminUser, "admin@123");


            builder.Entity<IdentityUser>().HasData(AdminUser);



            // Add All roles to AdminUser
            var AdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = AdminId,
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = AdminId,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(AdminRoles);

        }

    }
}
