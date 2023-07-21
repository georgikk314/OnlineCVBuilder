using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
    public class LanguageViewModel
    {
        [Required(ErrorMessage = "Language Name is required.")]
        [StringLength(100, ErrorMessage = "Language Name cannot exceed 100 characters.")]
        public string? LanguageName { get; set; }

        [StringLength(50, ErrorMessage = "Proficiency Level cannot exceed 50 characters.")]
        public string? ProficiencyLevel { get; set; }
    }
}
