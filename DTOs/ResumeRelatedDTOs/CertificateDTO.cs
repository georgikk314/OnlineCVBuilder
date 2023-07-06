namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class CertificateDTO
    {
        public int ResumeId { get; set; }
        public string CertificateName { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
