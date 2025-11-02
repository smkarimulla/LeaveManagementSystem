
using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
        //.ForMember(dest=> dest.Day, opt=>opt.MapFrom(src=>src.NumberOfDays));
        CreateMap<LeaveTypeCreateVM, LeaveType>();
        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();             
    }
}
