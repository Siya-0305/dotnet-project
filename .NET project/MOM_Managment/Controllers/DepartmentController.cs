using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace MOM_System.Controllers
{ 
    public class DepartmentController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region DepartmentList
        public IActionResult DepartmentList()
        {
            List<DepartmentViewModel> List = new List<DepartmentViewModel>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_SelectAll_MOM_Department";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DepartmentViewModel model = new DepartmentViewModel();
                model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                model.DepartmentName = reader["DepartmentName"].ToString();
                model.Created = Convert.ToDateTime(reader["Created"]);
                model.Modified = Convert.ToDateTime(reader["Modified"]);

                List.Add(model);

            }
            connection.Close();
            return View(List);

        }
        #endregion

        #region DepartmentAddEdit
        public IActionResult DepartmentAddEdit(int? id)
        {
            if (id == null)
            {
                return View(new DepartmentViewModel());
            }

            DepartmentViewModel model = new DepartmentViewModel();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_SelectByID_MOM_Department", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                    model.DepartmentName = reader["DepartmentName"].ToString();
                }
            }

            return View(model);
        }
        #endregion

        #region Saverecord
        [HttpPost]
        public IActionResult Save(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmentAddEdit", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                if (model.DepartmentID == 0)
                {
                    cmd.CommandText = "SP_Insert_MOM_Department";
                }
                else
                {
                    cmd.CommandText = "SP_Update_MOM_Department";
                    cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                }

                cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Department saved successfully!";
            return RedirectToAction("DepartmentList");
        }
        #endregion

        #region Deleterecord
        [HttpPost]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_Delete_MOM_Department", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Department deleted successfully!";
            return RedirectToAction("DepartmentList");
        }
        #endregion

    }
}
