using Microsoft.AspNetCore.Mvc;

namespace Mom_ManagmentSystem.Controllers
{
    public class MeetingController : Controller
    {
        public IActionResult MeetingList()
        {
            return View();
        }

        public IActionResult MeetingAddEdit()
        {
            return View();
        }
    }
}
