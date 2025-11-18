namespace LeaveManagementSystem.Application.Services.Periods
{
    public interface IPeriodsService
    {
        Task<bool> CheckIfPeriodExists(string name);
        Task<bool> CheckIfPeriodExistsForEdit(PeriodVM periodEditVM);
        Task Create(PeriodVM periodCreateVM);
        Task Delete(int id);
        Task Edit(PeriodVM periodEditVM);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<PeriodVM>> GetAll();
        Task<Period> GetCurrentPeriod();
        bool PeriodExists(int id);
    }
}