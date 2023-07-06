namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class ResumeDTO
    {
        public int ResumeId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public PersonalInfoDTO PersonalInfo { get; set; }
        public List<EducationDTO> Educations { get; set; }
        public List<WorkExperienceDTO> WorkExperiences { get; set; }
        public List<SkillDTO> Skills { get; set; }
        public List<LanguageDTO> Languages { get; set; }
        public List<LocationDTO> Locations { get; set; }
        public List<CertificateDTO> Certificates { get; set; }
    }
}
