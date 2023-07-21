namespace Online_CV_Builder.Data.Entities
{
    public class ResumeTemplates
    {
        public int? ResumeId { get; set; }
        public int? TemplateId { get; set; }
        public virtual Resumes? Resume { get; set; }
        public virtual Templates? Template { get; set; }
    }
}
