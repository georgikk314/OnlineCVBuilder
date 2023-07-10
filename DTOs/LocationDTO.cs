using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class LocationDTO
    {
        public int ResumeId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
        public string? State { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; }
    }
}
