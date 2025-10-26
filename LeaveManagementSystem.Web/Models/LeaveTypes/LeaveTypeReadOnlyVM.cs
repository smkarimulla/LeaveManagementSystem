using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM: BaseLeaveTypes
    {
        public string Name { get; set; } = string.Empty;
        [Display(Name ="Maximum Allocation Days")]
        public int NumberOfDays { get; set; }
    }
}
