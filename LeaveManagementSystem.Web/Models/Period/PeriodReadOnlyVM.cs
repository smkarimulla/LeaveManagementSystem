namespace LeaveManagementSystem.Web.Models.Period
{
    public class PeriodReadOnlyVM: BasePeriodVM
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
