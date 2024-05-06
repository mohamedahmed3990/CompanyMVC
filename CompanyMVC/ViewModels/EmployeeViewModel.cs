using CompanyMVC.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(22,30)]
        public int? Age { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
