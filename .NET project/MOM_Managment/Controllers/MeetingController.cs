using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace MOM_System.Controllers
{
    public class MeetingController : Controller
    {
        #region configuration
        private readonly IConfiguration _configuration;

        public MeetingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region MeetingList
        public IActionResult MeetingList()
        {
            List<MeetingViewModel> List = new List<MeetingViewModel>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Meetings_SelectAll";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MeetingViewModel model = new MeetingViewModel();
                model.MeetingId = Convert.ToInt32(reader["MeetingId"]);
                model.MeetingTypeName = reader["MeetingTypeName"].ToString();
                model.MeetingDate = Convert.ToDateTime(reader["MeetingDate"]);
                model.MeetingDescription = reader["MeetingDescription"].ToString();
                model.DepartmentName = reader["DepartmentName"].ToString();
                model.MeetingVenueName = reader["MeetingVenueName"].ToString();

                List.Add(model);

            }
            connection.Close();
            return View(List);

        }

        #endregion

        #region MeetingAddEdit
        public IActionResult MeetingAddEdit(int? id)
        {
            if (id == null)
                return View(new MeetingViewModel());

            MeetingViewModel model = new();
            string cs = _configuration.GetConnectionString("ConnectionString");

            using SqlConnection con = new(cs);
            using SqlCommand cmd = new("PR_Meeting_SelectByPK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MeetingID", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                model.MeetingId = Convert.ToInt32(dr["MeetingID"]);
                model.MeetingDate = Convert.ToDateTime(dr["MeetingDate"]);
                model.MeetingVenueName = dr["MeetingVenueName"]?.ToString();
                model.MeetingTypeName = dr["MeetingTypeName"]?.ToString();
                model.DepartmentName = dr["DepartmentName"]?.ToString();
                model.IsCancelled = Convert.ToBoolean(dr["IsCancelled"]);
            }

            return View(model);
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MeetingViewModel model)
        {
            if (!ModelState.IsValid)
                return View("MeetingAddEdit", model);

            string cs = _configuration.GetConnectionString("ConnectionString");

            using SqlConnection con = new(cs);
            using SqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.MeetingId == 0)
            {
                cmd.CommandText = "PR_Meeting_Insert";
            }
            else
            {
                cmd.CommandText = "PR_Meeting_UpdateByPK";
                cmd.Parameters.AddWithValue("@MeetingID", model.MeetingId);
            }

            cmd.Parameters.AddWithValue("@MeetingDate", model.MeetingDate);
            cmd.Parameters.AddWithValue("@MeetingVenueName", model.MeetingVenueName);
            cmd.Parameters.AddWithValue("@MeetingTypeName", model.MeetingTypeName);
            cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
            cmd.Parameters.AddWithValue("@IsCancelled", model.IsCancelled);

            con.Open();
            cmd.ExecuteNonQuery();

            TempData["SuccessMessage"] = "Meeting saved successfully!";
            return RedirectToAction("MeetingList");
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string cs = _configuration.GetConnectionString("ConnectionString");

            using SqlConnection con = new(cs);
            using SqlCommand cmd = new("PR_Meeting_DeleteByPK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MeetingID", id);

            con.Open();
            cmd.ExecuteNonQuery();

            TempData["SuccessMessage"] = "Meeting deleted successfully!";
            return RedirectToAction("MeetingList");
        }
        #endregion
    }
}
