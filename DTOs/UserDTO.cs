using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.DTOs
{
    public class UserDTO
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
