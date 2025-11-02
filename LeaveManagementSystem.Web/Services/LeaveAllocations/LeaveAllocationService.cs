using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public class LeaveAllocationsService(ApplicationDbContext _context,
                                     IHttpContextAccessor _httpContextAccessor,
                                     UserManager<ApplicationUser> _userManager,
                                     IMapper _mapper) : ILeaveAllocationsService
{
    public async Task AllocateLeave(string employeeId)
    {
        // get all leave types
        var leaveTypes = await _context.LeaveTypes
                            .Where(q=> !q.LeaveAllocations.Any(x=>x.EmployeeId == employeeId))
                            .ToListAsync();

        // get the current period based on the year
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
        var monthsRemaining = period.EndDate.Month - currentDate.Month;

        // foreach leave type, create an allocation entry
        foreach (var leaveType in leaveTypes)
        {
            //var allocationExists = await AllocationExists(employeeId, period.Id, leaveType.Id);
            //if (allocationExists)
            //{
            //    continue;
            //}         //-------> Wokrs but not best practice
            var accurateRate = decimal.Divide(leaveType.NumberOfDays, 12);
            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                Days = (int)Math.Ceiling(accurateRate * monthsRemaining)
            };

            _context.Add(leaveAllocation);                
        }
        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
    {        
        var user = string.IsNullOrEmpty(userId) 
                    ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User)
                    : await _userManager.FindByIdAsync(userId);

        var allocations = await GetAllocations(user.Id);
        var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
        var leaveTypesCount = await _context.LeaveTypes.CountAsync();
        var employeeVM = new EmployeeAllocationVM
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVMList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count()
        };

        return employeeVM;
    }

    public async Task<List<EmployeeVM>> GetEmployees()
    {
        var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
        var employeeList = _mapper.Map<List<ApplicationUser>,List<EmployeeVM>>(users.ToList());

        return employeeList;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
    {
        var allocation = await _context.LeaveAllocations
                                .Include(q=>q.LeaveType)
                                .Include(q => q.Employee)
                                .FirstOrDefaultAsync(q => q.Id == allocationId);

        var model = _mapper.Map<LeaveAllocationEditVM>(allocation);
        return model;
    }

    public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
    {
        //var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id) ?? throw new Exception("Leave Allocation record doesn't exist");
        //leaveAllocation.Days = allocationEditVM.Days;
        //_context.Update(leaveAllocation); // option 1
        ////_context.Entry(leaveAllocation).State = EntityState.Modified; // option 2
        //await _context.SaveChangesAsync();
        // alternatively 
        await _context.LeaveAllocations
            .Where(q => q.Id == allocationEditVM.Id)
            .ExecuteUpdateAsync(s=> s.SetProperty(e=>e.Days, allocationEditVM.Days));

    }

    private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        var currentDate = DateTime.Now;

        var leaveAllocations = await _context.LeaveAllocations
                                .Include(q => q.LeaveType)
                                .Include(q => q.Period)
                                //.Include(q => q.Days)
                                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                                .ToListAsync();

        return leaveAllocations;
    }

    private async Task<bool> AllocationExists(string _userId, int periodId, int leaveTypeId)
    {
        var exists = await _context.LeaveAllocations.AnyAsync(q =>
                     q.EmployeeId == _userId
                     && q.LeaveTypeId == leaveTypeId
                     && q.PeriodId == periodId
        );
        return exists;
    }

}
