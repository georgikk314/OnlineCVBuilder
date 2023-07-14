using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_CV_Builder.Data.Entities
{
    public class Certificates
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("Resumes")]
        public int? ResumeId { get; set; }
        public string? CertificateName { get; set; }
        public string? IssuingOrganization { get; set; }
        public DateTime? IssueDate { get; set; }
    }
}
