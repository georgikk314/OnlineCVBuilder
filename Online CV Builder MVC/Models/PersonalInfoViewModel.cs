using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
	public class PersonalInfoViewModel
	{
		[Required(ErrorMessage = "Full Name is required.")]
		[StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
		public string FullName { get; set; }

		[StringLength(20, ErrorMessage = "Phone Number cannot exceed 20 characters.")]
		public string PhoneNumber { get; set; }

		[EmailAddress(ErrorMessage = "Invalid Email Address.")]
		public string Email { get; set; }
	}
}
