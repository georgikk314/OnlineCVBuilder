namespace Online_CV_Builder.DTOs.SharingRelatedDTOs
{
    public class ResumeSharingDTO
    {
        public int ResumeId { get; set; }
        public string RecipientEmail { get; set; }
        public string EmailUsername { get; set; }
        public string Message { get; set; }
        public string EmailPassword { get; set; }
    }
}
