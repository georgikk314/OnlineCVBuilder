using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
    public class SkillViewModel
    {
        [Required(ErrorMessage = "Skill Name is required.")]
        [StringLength(200, ErrorMessage = "Skill Name cannot exceed 200 characters.")]
        public string SkillName { get; set; }
    }
}
