using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<List<LeaveRequestsListVM>> GetEmployeeLeaveRequests();
        Task<LeaveRequestsListVM> GetAllLeaveRequests();
        Task CancelLeaveRequests(int leaveRequestId);
        Task ReviewLeaveRequests(ReviewLeaveRequestVM model);

        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
    }
}