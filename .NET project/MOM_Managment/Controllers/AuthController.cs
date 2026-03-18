using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace Mom_Managment.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new UserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string sqlConnString = _configuration.GetConnectionString("ConnectionString");
                bool isValidUser = false;

                using (var sqlConnection = new SqlConnection(sqlConnString))
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_MST_User_SelectForLogin";
                    sqlCommand.Parameters.AddWithValue("@Username", model.Username);
                    sqlCommand.Parameters.AddWithValue("@Password", model.Password);

                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            isValidUser = true;
                        }
                    }
                }

                if (isValidUser)
                {
                    HttpContext.Session.SetString("UserName", model.Username);
                    return RedirectToAction("DepartmentList", "Department");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}