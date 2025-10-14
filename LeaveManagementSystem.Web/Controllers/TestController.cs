using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    public class TestController : Controller
    {        
        public ActionResult Index()
        {
            var modelName = new Models.TestViewModel
            {
                Name = "Shaik Mulla Karimulla",
                DateOfBirth = new DateOnly(2001,1,1)
            };
            return View(modelName);
        }       
    }
}
