using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
    }
}