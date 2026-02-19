using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace Mom_Managment.Controllers
{
    public class MeetingTypeController : Controller
    {
        #region configuration
        private readonly IConfiguration _configuration;

        public MeetingTypeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region MeetingList
        public IActionResult MeetingTypeList()
        {
            List<MeetingTypeViewModel> List = new List<MeetingTypeViewModel>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_SelectAll_MOM_MeetingType";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MeetingTypeViewModel model = new MeetingTypeViewModel();
                model.MeetingTypeId = Convert.ToInt32(reader["MeetingTypeId"]);
                model.MeetingTypeName = reader["MeetingTypeName"].ToString();
                model.Remarks = reader["Remarks"].ToString();
                model.Created = Convert.ToDateTime(reader["Created"]);
                model.Modified = Convert.ToDateTime(reader["Modified"]);

                List.Add(model);

            }
            connection.Close();
            return View(List);
        }

        #endregion

        #region MeetingTypeAddEdit
        public IActionResult MeetingTypeAddEdit(int? id)
        {
            if (id == null)
            {
                return View(new MeetingTypeViewModel());
            }

            MeetingTypeViewModel model = new MeetingTypeViewModel();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_SelectByID_MOM_MeetingType", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingTypeId", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model.MeetingTypeId = Convert.ToInt32(reader["MeetingTypeId"]);
                    model.MeetingTypeName = reader["MeetingTypeName"].ToString();
                    model.Remarks = reader["Remarks"].ToString();
                }
            }

            return View(model);
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MeetingTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MeetingTypeAddEdit", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                if (model.MeetingTypeId == 0)
                {
                    cmd.CommandText = "SP_Insert_MOM_MeetingType";
                }
                else
                {
                    cmd.CommandText = "SP_Update_MOM_MeetingType";
                    cmd.Parameters.AddWithValue("@MeetingTypeId", model.MeetingTypeId);
                }

                cmd.Parameters.AddWithValue("@MeetingTypeName", model.MeetingTypeName);
                cmd.Parameters.AddWithValue("@Remarks", model.Remarks);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Meeting type saved successfully!";
            return RedirectToAction("MeetingTypeList");
        }
        #endregion

        #region Deleterecord
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_Delete_MOM_MeetingType", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingTypeId", id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Meeting type deleted successfully!";
            return RedirectToAction("MeetingTypeList");
        }
        #endregion
    }
}
