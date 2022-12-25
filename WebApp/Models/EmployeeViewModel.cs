using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage =("Please enter address"))]
        public string Address { get; set; }
        [Required(ErrorMessage ="Please select department")]
        [Display(Name= "Department")]
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
