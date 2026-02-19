using System;
using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class MeetingVenueViewModel
    {
        [Key]
        public int MeetingVenueId { get; set; }

        [Required(ErrorMessage = "Venue name is required.")]
        public string MeetingVenueName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }
    }
}