using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class MeetingMemberViewModel
    {
        public int MeetingMemberID { get; set; }

        [Required(ErrorMessage = "Meeting is required")]
        public int MeetingID { get; set; }

        [Required(ErrorMessage = "Staff is required")]
        public int StaffID { get; set; }

        public string? MeetingDescription { get; set; }
        public string? StaffName { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string? EmailAddress { get; set; }


        public bool IsPresent { get; set; }
        public string? Remarks { get; set; }
    }
}
