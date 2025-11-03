using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public class LeaveRequestsService : ILeaveRequestsService
    {
        public Task CancelLeaveRequests(int leaveRequestId)
        {
            throw new NotImplementedException();
        }

        public Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestsListVM> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public Task ReviewLeaveRequests(ReviewLeaveRequestVM model)
        {
            throw new NotImplementedException();
        }
    }
}
