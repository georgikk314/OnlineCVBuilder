using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.Data.Entities
{
    public class Templates
    {
        [Key]
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string TemplateFilePath { get; set; }
        public virtual ICollection<ResumeTemplates> ResumeTemplates { get; set; }
    }
}

