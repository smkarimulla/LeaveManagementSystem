using LeaveManagementSystem.Web.Data;

namespace LeaveManagementSystem.Web.Services.LeaveRequests;

public class LeaveRequestsService(IMapper _mapper
                                    , ApplicationDbContext _context
                                    , UserManager<ApplicationUser> _userManager
                                    , IHttpContextAccessor _httpContextAccessor) : ILeaveRequestsService
{
    public async Task CancelLeaveRequests(int leaveRequestId)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

        // restore allocation days based on restart 
        var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
        var allocation = await _context.LeaveAllocations
                                    .FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId 
                                    && q.EmployeeId == leaveRequest.EmployeeId);

        allocation.Days += numberOfDays;

        await _context.SaveChangesAsync();
    }

    public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
    {
        // Map the data to leave request data model
        var leaveRequest = _mapper.Map<LeaveRequest>(model);

        // get logged in employeeId
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        leaveRequest.EmployeeId = user.Id;

        // Set LeaveRequestId to pending
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

        // Save Leave Request 
        _context.Add(leaveRequest);            

        // Deduct allocation days based on request
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var allocationToDeduct = await _context.LeaveAllocations
                                    .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);

        allocationToDeduct.Days -= numberOfDays;

        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeLeaveRequestsListVM> AdminGetAllLeaveRequests()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(q => q.LeaveType)                
            .ToListAsync();

        var approvedLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved);
        var pendingLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending);
        var cancelledLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Canceled);

        var leaveRequestModel = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
        {
            StartDate = q.StartDate,
            EndDate = q.EndDate,
            Id = q.Id,
            LeaveType = q.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
            NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
        }).ToList();

        var model = new EmployeeLeaveRequestsListVM
        {
            ApprovedRequests = approvedLeaveRequestsCount,
            PendingRequests = pendingLeaveRequestsCount,
            CancelledRequests = cancelledLeaveRequestsCount,
            TotalRequests = leaveRequests.Count,
            LeaveRequests = leaveRequestModel

        };

        return model;
    }

    // Get EMployee Leave Requests
    public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var leaveRequests = await _context.LeaveRequests
            .Include(q=>q.LeaveType)
            .Where(q => q.EmployeeId == user.Id)
            .ToListAsync();

        var model = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
        {
            StartDate = q.StartDate,
            EndDate = q.EndDate,
            Id = q.Id,
            LeaveType = q.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
            NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
        }).ToList();

        return model;
    }

    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
    {
        // get logged in employeeId
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        // number of days
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        // diff
        var allocationToDeduct = await _context.LeaveAllocations
                                    .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);

        return allocationToDeduct.Days < numberOfDays;
    }

    public async Task ReviewLeaveRequests(int leaveRequestId, bool approved)
    {
        var loggedInUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequestStatusId = approved ? (int)LeaveRequestStatusEnum.Approved:(int)LeaveRequestStatusEnum.Canceled;

        leaveRequest.ReviewerId = loggedInUser.Id;

        if(!approved)
        {
            // restore allocation days based on restart 
            var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations
                                        .FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId
                                        && q.EmployeeId == leaveRequest.EmployeeId);

            allocation.Days += numberOfDays;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(q=>q.LeaveType)
            .FirstAsync(q=>q.Id == id);
        var loggedInUser = await _userManager.FindByIdAsync(leaveRequest.EmployeeId);
        var model = new ReviewLeaveRequestVM
        {
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            Id = leaveRequest.Id,
            LeaveType = leaveRequest.LeaveType.Name,
            RequestComments = leaveRequest.RequestComments,
            LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
            NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
            Employee = new EmployeeVM
            {
                Id = leaveRequest.EmployeeId,
                Email = loggedInUser.Email,
                FirstName = loggedInUser.FirstName,
                LastName = loggedInUser.LastName
            }
        };
        return model;

    }
}
