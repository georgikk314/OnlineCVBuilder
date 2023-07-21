using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_CV_Builder.Data.Entities
{
    public class PersonalInfo
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("Resumes")]
        public int? ResumeId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
