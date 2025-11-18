using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Users;

namespace LeaveManagementSystem.Application.Services.LeaveRequests;

public class LeaveRequestsService(IMapper _mapper
                                    , ApplicationDbContext _context
                                    , IUserService _userService
                                    , ILeaveAllocationsService _leaveAllocationsService) : ILeaveRequestsService
{
    public async Task CancelLeaveRequests(int leaveRequestId)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

        // restore allocation days based on restart 
        await UpdateAllocationDays(leaveRequest, false);
        await _context.SaveChangesAsync();
    }

    public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
    {
        // Map the data to leave request data model
        var leaveRequest = _mapper.Map<LeaveRequest>(model);

        // get logged in employeeId
        var user = await _userService.GetLoggedInUser();
        leaveRequest.EmployeeId = user.Id;

        // Set LeaveRequestId to pending
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

        // Save Leave Request 
        _context.Add(leaveRequest);

        // Deduct allocation days based on request
        await UpdateAllocationDays(leaveRequest, true);
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
        var user = await _userService.GetLoggedInUser();
        var leaveRequests = await _context.LeaveRequests
            .Include(q => q.LeaveType)
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
        // get logged in user
        var user = await _userService.GetLoggedInUser();
        // number of days
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        // get the current period based on the year
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
        // diff
        var allocationToDeduct = await _context.LeaveAllocations
                                    .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId
                                    && q.EmployeeId == user.Id
                                    && q.PeriodId == period.Id);

        return allocationToDeduct.Days < numberOfDays;
    }

    public async Task ReviewLeaveRequests(int leaveRequestId, bool approved)
    {
        var loggedInUser = await _userService.GetLoggedInUser();
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequestStatusId = approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Canceled;

        leaveRequest.ReviewerId = loggedInUser.Id;

        if (!approved)
        {
            await UpdateAllocationDays(leaveRequest, false);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstAsync(q => q.Id == id);
        var loggedInUser = await _userService.GetUserById(leaveRequest.EmployeeId);
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

    private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
    {
        var allocation = await _leaveAllocationsService.GetCurrentAllocation(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
        var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);

        if (deductDays)
        {
            allocation.Days -= numberOfDays;
        }
        else
        {
            allocation.Days += numberOfDays;
        }

        _context.Entry(allocation).State = EntityState.Modified;
    }

    private int CalculateDays(DateOnly start, DateOnly end)
    {
        return end.DayNumber - start.DayNumber;
    }
}
