using LeaveManagementSystem.Application.Models.LeaveAllocations;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class LeaveAllocationAutoMapper : Profile
    {

        public LeaveAllocationAutoMapper()
        {
            // Auto mapper for Period
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>();
            CreateMap<ApplicationUser, EmployeeVM>();
            CreateMap<Period, PeriodVM>().ReverseMap();
        }
    }
}
