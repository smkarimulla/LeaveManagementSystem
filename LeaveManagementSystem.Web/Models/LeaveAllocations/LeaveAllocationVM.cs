namespace LeaveManagementSystem.Web.Models.LeaveAllocations
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }

        [Display(Name = "Number Of Days")]
        public int Days { get; set; }

        [Display(Name = "Allocation Period")]
        public PeriodVM? Period { get; set; }

        public LeaveTypeReadOnlyVM LeaveType { get; set; }
    }
}
