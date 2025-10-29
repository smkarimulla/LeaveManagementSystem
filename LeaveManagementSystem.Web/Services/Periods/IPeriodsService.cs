namespace LeaveManagementSystem.Web.Services.Periods
{
    public interface IPeriodsService
    {
        Task<bool> CheckIfPeriodExists(string name);
        Task<bool> CheckIfPeriodExistsForEdit(PeriodEditVM periodEditVM);
        Task Create(PeriodCreateVM periodCreateVM);
        Task Delete(int id);
        Task Edit(PeriodEditVM periodEditVM);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<PeriodReadOnlyVM>> GetAll();
        bool PeriodExists(int id);
    }
}