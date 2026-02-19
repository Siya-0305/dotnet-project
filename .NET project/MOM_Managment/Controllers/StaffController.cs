using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace MOM_System.Controllers
{
    public class StaffController : Controller
    {
        #region configuration
        private readonly IConfiguration _configuration;

        public StaffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region StaffList
        public IActionResult StaffList()
        {
            List<StaffModelView> List = new List<StaffModelView>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Staff_SelectAll";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                StaffModelView model = new StaffModelView();
                model.StaffId = Convert.ToInt32(reader["StaffId"]);
                model.StaffName = reader["StaffName"].ToString();
                model.DepartmentName = (reader["DepartmentName"].ToString());
                model.MobileNo = (reader["MobileNo"].ToString());
                model.EmailAddress = (reader["EmailAddress"].ToString());

                List.Add(model);

            }
            connect.Close();
            return View(List);

        }

        #endregion

        #region StaffAddEdit
        public IActionResult StaffAddEdit(int? id)
        {
            if (id == null)
            {
                return View(new StaffModelView());
            }

            StaffModelView model = new StaffModelView();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Staff_SelectByPK", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffID", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model.StaffId = Convert.ToInt32(reader["StaffID"]);
                    model.StaffName = reader["StaffName"].ToString();
                    model.DepartmentName = reader["DepartmentName"].ToString();
                    model.MobileNo = reader["MobileNo"].ToString();
                    model.EmailAddress = reader["EmailAddress"].ToString();
                }
            }

            return View(model);
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(StaffModelView model)
        {
            if (!ModelState.IsValid)
            {
                return View("StaffAddEdit", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                if (model.StaffId == 0)
                {
                    cmd.CommandText = "PR_Staff_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_Staff_UpdateByPK";
                    cmd.Parameters.AddWithValue("@StaffID", model.StaffId);
                }

                cmd.Parameters.AddWithValue("@StaffName", model.StaffName);
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                cmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);

                cmd.Parameters.AddWithValue("@Remarks",
    string.IsNullOrEmpty(model.Remarks) ? DBNull.Value : model.Remarks);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Staff saved successfully!";
            return RedirectToAction("StaffList");
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Staff_DeleteByPK", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffID", id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Staff deleted successfully!";
            return RedirectToAction("Index");
        }
        #endregion
    }
}
