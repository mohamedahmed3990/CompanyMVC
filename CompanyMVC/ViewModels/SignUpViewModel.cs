using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "First name is required")]
		public string FName { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		public string LName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


		[Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare(nameof(Password) , ErrorMessage = "Confirm Password dose not match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }


        public bool IsAgree { get; set; }
    } 
}
