namespace Online_CV_Builder.Data.Entities
{
    public class ResumeLocations
    {
        public int ResumeId { get; set; }
        public int LocationId { get; set; }
        public virtual Resumes Resume { get; set; }
        public virtual Locations Location { get; set; }
    }
}
