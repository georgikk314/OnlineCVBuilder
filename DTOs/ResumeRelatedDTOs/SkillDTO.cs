using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class SkillDTO
    {
        public int ResumeId { get; set; }

        [Required(ErrorMessage = "Skill Name is required.")]
        [StringLength(200, ErrorMessage = "Skill Name cannot exceed 200 characters.")]
        public string SkillName { get; set; }
    }
}
