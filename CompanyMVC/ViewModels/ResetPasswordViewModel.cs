using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password dose not match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }


    }
}
