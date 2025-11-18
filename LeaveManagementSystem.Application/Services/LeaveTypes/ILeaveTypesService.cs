namespace LeaveManagementSystem.Application.Services.LeaveTypes;

public interface ILeaveTypesService
{
    Task<bool> CheckLeaveTypeNameExists(string name);
    Task<bool> CheckLeaveTypeNameExistsinEdit(LeaveTypeEditVM leaveTypeEditVM);
    Task Create(LeaveTypeCreateVM model);
    Task<bool> DaysExceededMaximum(int leaveTypeId, int days);
    Task Edit(LeaveTypeEditVM model);
    Task<T?> Get<T>(int id) where T : class;
    Task<List<LeaveTypeReadOnlyVM>> GetAll();
    bool LeaveTypeExists(int id);
    Task Remove(int id);
}