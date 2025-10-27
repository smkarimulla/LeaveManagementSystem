using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
               new IdentityRole 
               { 
                    Id= "bf987263-05fc-41e9-9cd8-e81527d9dcf3",
                    Name="Employee",
                    NormalizedName="EMPLOYEE"
               },
               new IdentityRole 
               {
                   Id = "72fd8756-84aa-40c6-aece-86163790ec34",
                   Name = "Supervisor",
                   NormalizedName = "SUPERVISOR"
               },
               new IdentityRole 
               {
                   Id = "e587f44c-3215-4d4f-a21c-5f6a72151d0b",
                   Name = "Administrator",
                   NormalizedName = "ADMINISTRATOR"
               }
             );

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id= "3b380f13-8a61-4b26-9658-05dac0aff78a",
                    Email="admin@localhost.com",
                    NormalizedEmail="ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName="admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                }
             );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e587f44c-3215-4d4f-a21c-5f6a72151d0b",
                    UserId = "3b380f13-8a61-4b26-9658-05dac0aff78a"
                }
                
             );
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
