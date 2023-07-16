using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_CV_Builder.Data.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users User { get; set; }
    }
}
