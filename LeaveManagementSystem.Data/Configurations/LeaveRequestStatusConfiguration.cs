using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class LeaveRequestStatusConfiguration : IEntityTypeConfiguration<LeaveRequestStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
        {
            builder.HasData(new LeaveRequestStatus
            {
                Id = 1,
                Name = "Pending"
            });
            builder.HasData(new LeaveRequestStatus
            {
                Id = 2,
                Name = "Approved"
            });
            builder.HasData(new LeaveRequestStatus
            {
                Id = 3,
                Name = "Declined"
            });
            builder.HasData(new LeaveRequestStatus
            {
                Id = 4,
                Name = "Canceled"
            });
        }
    }
}
