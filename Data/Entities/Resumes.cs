using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_CV_Builder.Data.Entities
{
    public class Resumes
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public virtual ICollection<ResumeSkills>? ResumeSkills { get; set; }
        public virtual ICollection<ResumeLanguages>? ResumeLanguages { get; set; }
        public virtual ICollection<ResumeLocations>? ResumeLocations { get; set; }
        public virtual ICollection<ResumeTemplates>? ResumeTemplates { get; set; }
    }
}
