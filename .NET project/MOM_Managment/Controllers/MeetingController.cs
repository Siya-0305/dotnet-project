using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

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
        [HttpGet]
        public IActionResult MeetingList()
        {
            List<MeetingViewModel> List = GetMeeting(null);
            //string connectionString = _configuration.GetConnectionString("ConnectionString");
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();

            //SqlCommand cmd = connection.CreateCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "PR_Meetings_SelectAll";
            //SqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    MeetingViewModel model = new MeetingViewModel();
            //    model.MeetingId = Convert.ToInt32(reader["MeetingId"]);
            //    model.MeetingTypeName = reader["MeetingTypeName"].ToString();
            //    model.MeetingDate = Convert.ToDateTime(reader["MeetingDate"]);
            //    model.MeetingDescription = reader["MeetingDescription"].ToString();
            //    model.DepartmentName = reader["DepartmentName"].ToString();
            //    model.MeetingVenueName = reader["MeetingVenueName"].ToString();

            //    List.Add(model);

            //}
            //connection.Close();
            return View(List);

        }
        #endregion

        #region PostMeetingList
        [HttpPost]
        public IActionResult MeetingList(IFormCollection formData)
        {
            string searchText = formData["SearchText"].ToString();

            if (string.IsNullOrWhiteSpace(searchText))
                searchText = null;

            ViewBag.SearchText = searchText;

            List<MeetingViewModel> list = GetMeeting(searchText);
            return View(list);
        }
        #endregion

        #region GetMeeting
        private List<MeetingViewModel> GetMeeting(string searchText)
        {
            List<MeetingViewModel> list = new List<MeetingViewModel>();

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "PR_Meetings_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;

            if (searchText != null)
                cmd.Parameters.AddWithValue("@SearchText", searchText);
            else
                cmd.Parameters.AddWithValue("@SearchText", DBNull.Value);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MeetingViewModel m = new MeetingViewModel();
                m.MeetingId = Convert.ToInt32(reader["MeetingId"]);
                m.MeetingTypeName = reader["MeetingTypeName"].ToString();
                m.MeetingDate = Convert.ToDateTime(reader["MeetingDate"]);
                m.MeetingDescription = reader["MeetingDescription"].ToString();
                m.DepartmentName = reader["DepartmentName"].ToString();
                m.MeetingVenueName = reader["MeetingVenueName"].ToString();

                list.Add(m);
            }

            reader.Close();
            connection.Close();
            return list;
        }

        #endregion

        #region MeetingAddEdit
        public IActionResult MeetingAddEdit(int? id)
        {
            ViewBag.MeetingList = FillMeetingDropdown();
            ViewBag.MeetingTypeList = FillMeetingTypeDropDown();
            ViewBag.MeetingVenueList = FillMeetingVenueDropDown();

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

        #region MeetingDropdown
        [HttpPost]
        public List<SelectListItem> FillMeetingDropdown()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Meeting_DDL";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                List.Add(new SelectListItem(
                reader["MeetingTypeName"].ToString(),
                reader["MeetingID"].ToString()));

            }
            reader.Close();
            connect.Close();

            return List;
        }
        #endregion

        #region MeetingVenue Dropdown
        public List<SelectListItem> FillMeetingVenueDropDown()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MeetingVenue_DDL";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                List.Add(new SelectListItem(
                reader["MeetingVenueName"].ToString(),
                reader["MeetingVenueID"].ToString()));

            }
            reader.Close();
            connect.Close();

            return List;
        }
        #endregion

        #region MeetingType Dropdown
        public List<SelectListItem> FillMeetingTypeDropDown()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MOM_MeetingType_DDL";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                List.Add(new SelectListItem(
                reader["MeetingTypeName"].ToString(),
                reader["MeetingTypeID"].ToString()));

            }
            reader.Close();
            connect.Close();

            return List;
        }
        #endregion

    }
}
