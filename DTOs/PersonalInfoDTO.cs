﻿using System.ComponentModel.DataAnnotations;

namespace Online_CV_Builder.DTOs.ResumeRelatedDTOs
{
    public class PersonalInfoDTO
    {
        public int ResumeId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "Phone Number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }
}
