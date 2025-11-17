using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public class LeaveRequestsService(IMapper _mapper
                                        , ApplicationDbContext _context
                                        , UserManager<ApplicationUser> _userManager
                                        , IHttpContextAccessor _httpContextAccessor) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequests(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

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

        public Task<LeaveRequestsListVM> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        // Get EMployee Leave Requests
        public async Task<List<LeaveRequestsListVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var leaveRequests = await _context.LeaveRequests
                .Include(q=>q.LeaveType)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();

            var model = leaveRequests.Select(q => new LeaveRequestsListVM
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

        public Task ReviewLeaveRequests(ReviewLeaveRequestVM model)
        {
            throw new NotImplementedException();
        }
    }
}
