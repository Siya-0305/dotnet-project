using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class StaffModelView
    {
        [Key]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Staff name is required.")]
        public string StaffName { get; set; } = string.Empty;
        public int DepartmentID { get; set; }


        [Required(ErrorMessage = "Department name is required.")]
        public string DepartmentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid mobile number format.")]
        public string MobileNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; } = string.Empty;
        public string? Remarks { get; set; }

    }
}