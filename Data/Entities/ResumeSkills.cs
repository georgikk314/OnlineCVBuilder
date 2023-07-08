namespace Online_CV_Builder.Data.Entities
{
    public class ResumeSkills
    {
        public int? ResumeId { get; set; }
        public int? SkillId { get; set; }
        public virtual Resumes? Resume { get; set; }
        public virtual Skills? Skill { get; set; }
    }

    
}
