using System.ComponentModel.DataAnnotations;
namespace Online_CV_Builder.Data.Entities
{
    public class Users
    {
        [Key]
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
