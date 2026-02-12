using Microsoft.AspNetCore.Mvc;

namespace Mom_ManagmentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View("DepartmentList");
        }

        public IActionResult DepartmentAddEdit()
        {
            return View();
        }
    }
}
