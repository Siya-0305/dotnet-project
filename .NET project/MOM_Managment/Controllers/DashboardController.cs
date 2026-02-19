using Microsoft.AspNetCore.Mvc;

namespace Mom_Managment.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
