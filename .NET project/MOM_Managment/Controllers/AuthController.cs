using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace Mom_Managment.Controllers
{
    
    public class AuthController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserLogin(UserModel userLoginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = this._configuration.GetConnectionString("ConnectionString");

                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("PR_MST_User_SelectForLogin", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = userLoginModel.Username;
                    sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = userLoginModel.Password;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        HttpContext.Session.SetString("UserID", reader["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", reader["UserName"].ToString());

                        reader.Close();
                        sqlConnection.Close();

                        return RedirectToAction("DepartmentList", "Department");
                    }
                    else
                    {
                        reader.Close();
                        sqlConnection.Close();

                        TempData["ErrorMessage"] = "User is not found";
                        return RedirectToAction("Login", "Auth");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }

            return RedirectToAction("Login");
        }

        //public IActionResult Login(UserModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string sqlConnString = _configuration.GetConnectionString("ConnectionString");
        //        bool isValidUser = false;

        //        using (var sqlConnection = new SqlConnection(sqlConnString))
        //        using (var sqlCommand = sqlConnection.CreateCommand())
        //        {
        //            sqlCommand.CommandType = CommandType.StoredProcedure;
        //            sqlCommand.CommandText = "PR_MST_User_SelectForLogin";
        //            sqlCommand.Parameters.AddWithValue("@Username", model.Username);
        //            sqlCommand.Parameters.AddWithValue("@Password", model.Password);

        //            sqlConnection.Open();
        //            using (var reader = sqlCommand.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    isValidUser = true;
        //                }
        //            }
        //        }

        //        if (isValidUser)
        //        {
        //            HttpContext.Session.SetString("UserName", model.Username);
        //            return RedirectToAction("DepartmentList", "Department");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}