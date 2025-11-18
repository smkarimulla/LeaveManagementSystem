namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class LeaveRequestAutoMapper : Profile
    {
        public LeaveRequestAutoMapper()
        {
            CreateMap<LeaveRequestCreateVM, LeaveRequest>();

        }
    }

}
