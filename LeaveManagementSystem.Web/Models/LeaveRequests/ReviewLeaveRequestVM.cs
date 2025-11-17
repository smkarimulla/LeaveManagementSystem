namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeVM Employee { get; set; } = new EmployeeVM();
    }
}