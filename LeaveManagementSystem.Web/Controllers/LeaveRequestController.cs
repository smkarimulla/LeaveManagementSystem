using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers;

[Authorize]
public class LeaveRequestController(ILeaveTypesService _leaveTypeService
                                    ,ILeaveRequestsService _leaveRequestsService) : Controller
{
    // Employee View requests
    //LeaveRequest
    public async Task<IActionResult> Index()
    {
        var model = await _leaveRequestsService.GetEmployeeLeaveRequests();
        return View(model);
    }

    // Employee Create requests
    public async Task<IActionResult> Create()
    {
        var leaveTypes = await _leaveTypeService.GetAll();
        var leaveTypesLists = new SelectList(leaveTypes, "Id", "Name");
        var model = new LeaveRequestCreateVM
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            LeaveTypes = leaveTypesLists
        };
        return View(model);
    }

    // Employee Post Create requests
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequestCreateVM model)
    {
        // Validate that the number of days don't exceed the allocation
        if(await _leaveRequestsService.RequestDatesExceedAllocation(model))
        {
            ModelState.AddModelError(string.Empty, "You have exceeded your allocation");
            ModelState.AddModelError(nameof(model.EndDate), "The number of days requested is invalid");
        }

        if (ModelState.IsValid)
        {
            await _leaveRequestsService.CreateLeaveRequest(model);
            return RedirectToAction(nameof(Index));
        }
        var leaveTypes = await _leaveTypeService.GetAll();
        model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
        return View(model);
    }

    // Employee cancel requests
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        await _leaveRequestsService.CancelLeaveRequests(id);
        return RedirectToAction(nameof(Index));
    }

    // Admin/Supe review requests         
    public async Task<IActionResult> AllLeaveRequestsList()
    {
        var model = await _leaveRequestsService.AdminGetAllLeaveRequests();
        return View(model);
    }  
    // Admin/Supe review requests 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int id)
    {
        var model = await _leaveRequestsService.GetLeaveRequestForReview(id);
        return View(model);
    }

}
