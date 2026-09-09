using System.ComponentModel.DataAnnotations;
namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Department { get; set; }

        public string Designation { get; set; }

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }
    }
}
