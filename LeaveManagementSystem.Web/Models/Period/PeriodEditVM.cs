namespace LeaveManagementSystem.Web.Models.Period
{
    public class PeriodEditVM: BasePeriodVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
    }
}
