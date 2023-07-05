using System.ComponentModel.DataAnnotations;
namespace Online_CV_Builder.Data.Entities
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }
        public string SkillName { get; set; }
        public virtual ICollection<ResumeSkills> ResumeSkills { get; set; }
    }
}
