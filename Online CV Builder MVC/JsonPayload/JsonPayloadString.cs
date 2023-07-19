using Online_CV_Builder_MVC.Models;

namespace Online_CV_Builder_MVC.JsonPayload
{
	public class JsonPayloadString
	{
		public ResumeTitleViewModel ResumeTitlePayload { get; set; }
        public PersonalInfoViewModel PersonalInfoPayload { get; set; }
        public CertificateViewModel CertificatePayload { get; set; }
        public EducationViewModel EducationPayload { get; set; }
        public SkillViewModel SkillPayload { get; set; }
        public WorkExperienceViewModel WorkExperiencePayload { get; set; }

    }
}
