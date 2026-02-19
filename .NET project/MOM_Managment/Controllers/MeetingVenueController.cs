using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace Mom_Managment.Controllers
{
    public class MeetingVenueController : Controller
    {
        #region configuration
        private readonly IConfiguration _configuration;

        public MeetingVenueController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region MeetingVenueList
        public IActionResult MeetingVenueList()
        {
            List<MeetingVenueViewModel> List = new List<MeetingVenueViewModel>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_SelectAll_MOM_MeetingVenue";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                MeetingVenueViewModel model = new MeetingVenueViewModel();
                model.MeetingVenueId = Convert.ToInt32(reader["MeetingVenueId"]);
                model.MeetingVenueName = reader["MeetingVenueName"].ToString();

                model.Created = Convert.ToDateTime(reader["Created"]);
                model.Modified = Convert.ToDateTime(reader["Modified"]);

                List.Add(model);

            }
            connection.Close();
            return View(List);
        }

        #endregion

        #region MeetingAddEdit
        public IActionResult MeetingVenueAddEdit(int? id)
        {
            if (id == null)
            {
                return View(new MeetingVenueViewModel());
            }

            MeetingVenueViewModel model = new MeetingVenueViewModel();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_SelectByID_MOM_MeetingVenue", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingVenueId", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model.MeetingVenueId = Convert.ToInt32(reader["MeetingVenueId"]);
                    model.MeetingVenueName = reader["MeetingVenueName"].ToString();
                }
            }

            return View(model);
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MeetingVenueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MeetingVenueAddEdit", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                if (model.MeetingVenueId == 0)
                {
                    cmd.CommandText = "SP_Insert_MOM_MeetingVenue";
                }
                else
                {
                    cmd.CommandText = "SP_Update_MOM_MeetingVenue";
                    cmd.Parameters.AddWithValue("@MeetingVenueId", model.MeetingVenueId);
                }

                cmd.Parameters.AddWithValue("@MeetingVenueName", model.MeetingVenueName);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Meeting venue saved successfully!";
            return RedirectToAction("MeetingVenueList");
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_Delete_MOM_MeetingVenue", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingVenueId", id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "Meeting venue deleted successfully!";
            return RedirectToAction("MeetingVenueList");
        }
        #endregion
    }
}
