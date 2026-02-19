using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class MeetingTypeViewModel
    {
        [Key]
        public int MeetingTypeId { get; set; }

        [Required(ErrorMessage = "Meeting type name is required.")]
        public string MeetingTypeName { get; set; } = string.Empty;
        public string? Remarks { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }
    }
}