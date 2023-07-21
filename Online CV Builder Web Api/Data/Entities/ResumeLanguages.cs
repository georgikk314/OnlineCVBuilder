namespace Online_CV_Builder.Data.Entities
{
    public class ResumeLanguages
    {
        public int? ResumeId { get; set; }
        public int? LanguageId { get; set; }
        public virtual Resumes? Resume { get; set; }
        public virtual Languages? Language { get; set; }
    }
}
