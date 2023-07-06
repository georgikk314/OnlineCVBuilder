namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class EducationDTO
    {
        public int ResumeId { get; set; }
        public string InstituteName { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
