using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
	public class DownloadViewModel
	{
		[Required(ErrorMessage = "Resume title is required.")]
		public string ResumeTitle { get; set; }
		[Required(ErrorMessage = "Resume id is required.")]
		public int ResumeId { get; set; }
	}
}
