using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        // Employee View requests
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // Employee Create requests
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Employee Post Create requests
        [HttpPost]
        public async Task<IActionResult> Create(int create /* Use VM */)
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
