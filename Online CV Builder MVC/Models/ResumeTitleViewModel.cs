using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
	public class ResumeTitleViewModel
	{
		[Required]
		
        public string ResumeTitle { get; set; }
		
    }
}
