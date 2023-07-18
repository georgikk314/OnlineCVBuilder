using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder_MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
