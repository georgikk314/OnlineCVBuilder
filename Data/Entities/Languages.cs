using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.Data.Entities
{
    public class Languages
    {
        [Key]
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string ProficiencyLevel { get; set; }
        public virtual ICollection<ResumeLanguages> ResumeLanguages { get; set; }
    }
}
