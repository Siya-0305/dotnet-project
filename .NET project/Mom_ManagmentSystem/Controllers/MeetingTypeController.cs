using Microsoft.AspNetCore.Mvc;

namespace Mom_ManagmentSystem.Controllers
{
    public class MeetingTypeController : Controller
    {
        public IActionResult MeetingTypeList()
        {
            return View();
        }

        public IActionResult MeetingTypeAddEdit()
        {
            return View();
        }
    }
}
