namespace LeaveManagementSystem.Web.Services.Periods;

public class PeriodsService(ApplicationDbContext _context, IMapper _mapper) : IPeriodsService
{
    public async Task<List<PeriodVM>> GetAll()
    {
        var data = await _context.Periods.ToListAsync();
        var viewData = _mapper.Map<List<PeriodVM>>(data);
        return viewData;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = await _context.Periods.FirstOrDefaultAsync(q => q.Id == id);
        if (data == null)
        {
            return null;
        }
        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Create(PeriodVM periodCreateVM)
    {
        var period = _mapper.Map<Period>(periodCreateVM);
        _context.Add(period);
        await _context.SaveChangesAsync();
    }

    public async Task Edit(PeriodVM periodEditVM)
    {
        var period = _mapper.Map<Period>(periodEditVM);
        _context.Update(period);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var period = await _context.Periods.FirstOrDefaultAsync(q => q.Id == id);
        if (period != null)
        {
            _context.Periods.Remove(period);
            await _context.SaveChangesAsync();
        }
    }

    // Validation checks 
    public bool PeriodExists(int id)
    {
        return _context.Periods.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfPeriodExists(string name)
    {
        var lowerCaseName = name.ToLower();
        var checkExists = await _context.Periods.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
        return checkExists;
    }

    public async Task<bool> CheckIfPeriodExistsForEdit(PeriodVM periodEditVM)
    {
        var lowerCaseName = periodEditVM.Name.ToLower();
        var checkExists = await _context.Periods.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
        return checkExists;
    }

}
