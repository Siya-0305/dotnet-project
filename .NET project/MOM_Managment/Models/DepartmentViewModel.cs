using System;
using System.ComponentModel.DataAnnotations;

namespace Mom_Managment.Models
{
    public class DepartmentViewModel
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }
    }
}