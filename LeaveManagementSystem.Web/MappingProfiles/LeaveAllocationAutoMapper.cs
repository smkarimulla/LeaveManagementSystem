using LeaveManagementSystem.Web.Models.LeaveAllocations;
using AutoMapper;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveAllocationAutoMapper: Profile
    {

        public LeaveAllocationAutoMapper() 
        {
            // Auto mapper for Period
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<ApplicationUser, EmployeeVM>();
            CreateMap<Period, PeriodVM>().ReverseMap();
        }
    }
}
