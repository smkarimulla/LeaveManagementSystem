using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services;

public class LeaveTypesService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypesService
{

    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        // var data = SELECT * FROM LeaveTypes
        var data = await _context.LeaveTypes.ToListAsync();
        // Converted into auto mapper uing the AutoMapper 
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
        // retunr the view model to the view
        return viewData;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(q => q.Id == id);
        if (data == null)
        {
            return null;
        }
        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Remove(int id)
    {
        var data = _context.LeaveTypes.FirstOrDefault(q => q.Id == id);
        if (data != null)
        {
            _context.LeaveTypes.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Create(LeaveTypeCreateVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task Edit(LeaveTypeEditVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }
    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }
    public async Task<bool> CheckLeaveTypeNameExists(string name)
    {
        var lowerCaseName = name.ToLower();
        var checkExists = await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
        return checkExists;
    }

    public async Task<bool> CheckLeaveTypeNameExistsinEdit(LeaveTypeEditVM leaveTypeEditVM)
    {
        var lowerCaseName = leaveTypeEditVM.Name.ToLower();
        var checkExists = _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName) &&
                                !q.Id.Equals(leaveTypeEditVM.Id));
        return await checkExists;
    }
}

