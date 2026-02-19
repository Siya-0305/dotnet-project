using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mom_Managment.Models;
using System.Data;
using System.Data.SqlClient;

namespace MOM_System.Controllers
{
    public class MeetingMemberController : Controller
    {
        #region configuration
        private readonly IConfiguration _configuration;

        public MeetingMemberController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region MeetingMembersList
        public IActionResult MeetingMembersList()
        {
            List<MeetingMemberViewModel> list = new List<MeetingMemberViewModel>();

            string connectString = _configuration.GetConnectionString("ConnectionString"); SqlConnection connect = new SqlConnection(connectString);
            connect.Open();

            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MeetingMember_SelectAll";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MeetingMemberViewModel model = new MeetingMemberViewModel();
                //model.MeetingName = reader["Meeting"].ToString();
                model.DepartmentName = reader["DepartmentName"].ToString();
                model.Remarks = reader["Remarks"].ToString();
                model.MeetingMemberID = Convert.ToInt32(reader["MeetingMemberID"]);
                model.MeetingID = Convert.ToInt32(reader["MeetingID"]);
                model.StaffName = reader["StaffName"].ToString();
                model.IsPresent = reader.GetBoolean("IsPresent");

                list.Add(model);
            }
            connect.Close();
            return View(list);
        }

        #endregion

        #region MeetingMembersAddEdit
        public IActionResult MeetingMembersAddEdit(int? id)
        {
            if (id == null)
                return View(new MeetingMemberViewModel());

            MeetingMemberViewModel model = new();
            string cs = _configuration.GetConnectionString("ConnectionString");

            using SqlConnection con = new(cs);
            using SqlCommand cmd = new("PR_MeetingMember_SelectByPK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MeetingMemberID", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                model.MeetingMemberID = Convert.ToInt32(dr["MeetingMemberID"]);
                model.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                model.StaffID = Convert.ToInt32(dr["StaffID"]);
                model.IsPresent = Convert.ToBoolean(dr["IsPresent"]);
                model.Remarks = dr["Remarks"]?.ToString();
            }

            return View(model);
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MeetingMemberViewModel model)
        {
            if (!ModelState.IsValid)
                return View("MeetingMembersAddEdit", model);

            string cs = _configuration.GetConnectionString("ConnectionString");

            using SqlConnection con = new(cs);
            using SqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.MeetingMemberID == 0)
            {
                cmd.CommandText = "PR_MeetingMember_Insert";
                cmd.Parameters.AddWithValue("@MeetingID", model.MeetingID);
                cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
            }
            else
            {
                cmd.CommandText = "PR_MeetingMember_UpdateByPK";
                cmd.Parameters.AddWithValue("@MeetingMemberID", model.MeetingMemberID);
                cmd.Parameters.AddWithValue("@MeetingID", model.MeetingID);
                cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
            }

            cmd.Parameters.AddWithValue("@IsPresent", model.IsPresent);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks ?? "");

            con.Open();
            //cmd.ExecuteNonQuery();

            TempData["SuccessMessage"] = "Meeting member saved successfully!";
            return RedirectToAction("MeetingMembersList");
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            string cs = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using SqlConnection con = new(cs);
                using SqlCommand cmd = new("PR_MeetingMember_DeleteByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingMemberID", id);

                con.Open();
                cmd.ExecuteNonQuery();

                TempData["SuccessMessage"] = "Meeting member deleted successfully!";
            }
            catch (SqlException ex)
            {
                TempData["ErrorMessage"] = "Cannot delete this meeting member. " + ex.Message;
            }

            return RedirectToAction("MeetingMembersList");
        }
        #endregion
    }
}
