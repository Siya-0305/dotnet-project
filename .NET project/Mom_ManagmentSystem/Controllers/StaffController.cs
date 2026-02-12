using Microsoft.AspNetCore.Mvc;

namespace Mom_ManagmentSystem.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult StaffList()
        {
            return View();
        }

        public IActionResult StaffAddEdit()
        {
            return View();
        }
    }
}
