﻿namespace Online_CV_Builder.DTOs
{
    public class WorkExperienceDTO
    {
        public int ResumeId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
