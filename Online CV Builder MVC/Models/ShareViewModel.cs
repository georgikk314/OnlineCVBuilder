using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
	public class ShareViewModel
	{
		[Required(ErrorMessage = "Resume title is required.")]
		public string ResumeTitle { get; set; }
		[EmailAddress(ErrorMessage = "Invalid Email Address.")]
		public string RecipientEmail { get; set; }
	}
}
