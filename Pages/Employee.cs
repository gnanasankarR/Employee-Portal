using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApp.Models
{
    [Table("company_employee_details")] // Ensure table name matches DB
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }  // Ensure EmployeeID is int

        [Required]
        [Column("EmployeeName")]
        public string EmployeeName { get; set; } = string.Empty;  // ✅ Default value added

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required]
        [EmailAddress]
        [Column("Email")]
        public string Email { get; set; } = string.Empty;  // ✅ Default value added
    }
}
