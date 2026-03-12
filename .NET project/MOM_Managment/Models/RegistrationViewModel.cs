using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string StaffName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, Phone]
        public string MobileNo { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
