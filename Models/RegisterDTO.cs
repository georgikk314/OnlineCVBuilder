using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Online_CV_Builder.Models
{
    public class RegisterDTO
    {
        public string Endpoint = "/register";
        public int Id { get; set; }
        [Required, NotNull]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress, NotNull]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, NotNull]
        [MinLength(10), MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
