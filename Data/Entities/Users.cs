using System.ComponentModel.DataAnnotations;
namespace Online_CV_Builder.Data.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        // public string PasswordHash { get; set; } = string.Empty;
        // public string PasswordSalt { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = new byte[64];
        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[64];
        public string? VerificationToken { get; set; }
        public DateTime VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime ResetTokenExpires { get; set; }
    }
}
