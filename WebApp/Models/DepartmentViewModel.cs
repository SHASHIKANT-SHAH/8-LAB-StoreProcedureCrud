using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
    }
}
