using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.Data.Entities
{
    public class Locations
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
        public virtual ICollection<ResumeLocations> ResumeLocations { get; set; }
    }
}
