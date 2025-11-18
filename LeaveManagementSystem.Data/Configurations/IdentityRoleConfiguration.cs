using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Id = "bf987263-05fc-41e9-9cd8-e81527d9dcf3",
                   Name = "Employee",
                   NormalizedName = "EMPLOYEE"
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
        }
    }
}
