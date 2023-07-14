using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_CV_Builder.Data.Entities
{
    public class WorkExperience
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("Resumes")]
        public int? ResumeId { get; set; }
        public string? CompanyName { get; set; }
        public string? Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
