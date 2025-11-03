using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveRequestAutoMapper : Profile
    {
        public LeaveRequestAutoMapper()
        {            
            CreateMap<LeaveRequestCreateVM, LeaveRequest>();          

        }
    }

}
