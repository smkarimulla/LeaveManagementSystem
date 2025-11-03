using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequests();
        Task<LeaveRequestsListVM> GetAllLeaveRequests();
        Task CancelLeaveRequests(int leaveRequestId);
        Task ReviewLeaveRequests(ReviewLeaveRequestVM model);
    }
}