
namespace LeaveManagementSystem.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
        //.ForMember(dest=> dest.Day, opt=>opt.MapFrom(src=>src.NumberOfDays));
        CreateMap<LeaveTypeCreateVM, LeaveType>();
        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();

        // Auto mapper for Period
        CreateMap<Period, PeriodReadOnlyVM>();
        CreateMap<PeriodCreateVM, Period>();
        CreateMap<PeriodEditVM, Period>().ReverseMap();
    }
}
