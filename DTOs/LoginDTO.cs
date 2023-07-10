using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.Models
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(10), MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}