using System;
using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class MeetingViewModel
    {
        [Key]
        public int MeetingId { get; set; }

        [Required(ErrorMessage = "Meeting date is required.")]
        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [Required(ErrorMessage = "Meeting venue is required.")]
        public string MeetingVenueName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Meeting type is required.")]
        public string MeetingTypeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department name is required.")]
        public string DepartmentName { get; set; } = string.Empty;
        public string MeetingDescription { get; set; } = string.Empty;

        [Required]
        public bool IsCancelled { get; set; }
      
    }
}