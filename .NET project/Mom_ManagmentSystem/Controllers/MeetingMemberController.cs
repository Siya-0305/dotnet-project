using Microsoft.AspNetCore.Mvc;

namespace Mom_ManagmentSystem.Controllers
{
    public class MeetingMemberController : Controller
    {
        public IActionResult MeetingMemberList()
        {
            return View();
        }

        public IActionResult MeetingMemberAddEdit()
        {
            return View();
        }
    }
}
