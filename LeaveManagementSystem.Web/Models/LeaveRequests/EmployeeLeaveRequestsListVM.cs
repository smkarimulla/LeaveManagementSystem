namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class EmployeeLeaveRequestsListVM
    {
        [Display(Name = "Total Number Of Requests")]
        public int TotalRequests { get; set; }

        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }

        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }

        [Display(Name = "Rejected Requests")]
        public int CancelledRequests { get; set; }

        public List<LeaveRequestReadOnlyVM> LeaveRequests { get; set; } = [];
    }
}