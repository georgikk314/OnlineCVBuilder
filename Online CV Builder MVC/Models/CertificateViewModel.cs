using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
	public class CertificateViewModel
	{
		[StringLength(100, ErrorMessage = "Certificate Name cannot exceed 100 characters.")]
		public string CertificateName { get; set; }

		[StringLength(100, ErrorMessage = "Issuing Organization cannot exceed 100 characters.")]
		public string IssuingOrganization { get; set; }
		public DateTime IssueDate { get; set; }
	}
}
