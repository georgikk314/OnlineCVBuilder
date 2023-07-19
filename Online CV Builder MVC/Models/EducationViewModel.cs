using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
	public class EducationViewModel
	{
		[Required(ErrorMessage = "Institute Name is required.")]
		[StringLength(200, ErrorMessage = "Institute Name cannot exceed 200 characters.")]
		public string InstituteName { get; set; }

		[StringLength(100, ErrorMessage = "Degree cannot exceed 100 characters.")]
		public string Degree { get; set; }

		[StringLength(100, ErrorMessage = "Field of Study cannot exceed 100 characters.")]
		public string FieldOfStudy { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
