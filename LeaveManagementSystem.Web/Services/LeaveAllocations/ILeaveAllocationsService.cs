
using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeave(string employeeId);    
    Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId);
    Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);
    Task<List<EmployeeVM>> GetEmployees();
    Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId);
    Task EditAllocation(LeaveAllocationEditVM allocationEditVM);
}