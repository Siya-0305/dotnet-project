using Microsoft.AspNetCore.Mvc;

namespace Mom_ManagmentSystem.Controllers
{
    public class MeetingVenueController : Controller
    {
        public IActionResult MeetingVenueList()
        {
            return View();
        }

        public IActionResult MeetingVenueAddEdit()
        {
            return View();
        }
    }
}
