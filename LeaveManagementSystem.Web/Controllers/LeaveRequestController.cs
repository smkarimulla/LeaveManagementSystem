using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestController(ILeaveTypesService _leaveTypeService
                                        ,ILeaveRequestsService leaveRequestsService) : Controller
    {
        // Employee View requests
        public async Task<IActionResult> Index()
        {
            return View();
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
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            return View();
        }

        // Employee cancel requests
        [HttpPost]
        public async Task<IActionResult> Cancel(int leaveRequestId)
        {
            return View();
        }

        // Admin/Supe review requests         
        public async Task<IActionResult> ListRequests()
        {
            return View();
        }

        // Employee Create requests
        public async Task<IActionResult> Review(int leaveRequestId)
        {
            return View();
        }

        // Admin/Supe review requests 
        [HttpPost]        
        public async Task<IActionResult> Review(/*Use View Model*/)
        {
            return View();
        }

    }
}
