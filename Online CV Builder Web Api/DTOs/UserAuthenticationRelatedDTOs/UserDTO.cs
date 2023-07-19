using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs
{
    public class UserDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Password { get; set; }
    }
}
