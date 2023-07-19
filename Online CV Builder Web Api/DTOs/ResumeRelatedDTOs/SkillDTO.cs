using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class SkillDTO
    {
        [StringLength(200, ErrorMessage = "Skill Name cannot exceed 200 characters.")]
        public string SkillName { get; set; }
    }
}
