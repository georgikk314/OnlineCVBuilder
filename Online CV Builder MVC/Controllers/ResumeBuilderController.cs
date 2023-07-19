using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder_MVC.JsonPayload;
using Online_CV_Builder_MVC.Models;

namespace Online_CV_Builder_MVC.Controllers
{
	public class ResumeBuilderController : Controller
	{
		private JsonPayloadString _jsonPayloadString;
        public ResumeBuilderController(JsonPayloadString jsonPayloadString)
        {
			_jsonPayloadString = jsonPayloadString;
        }
        public IActionResult ResumeTitle(ResumeTitleViewModel model) 
		{
			if(ModelState.IsValid)
			{
				_jsonPayloadString.ResumeTitlePayload = model;
				return RedirectToAction("PersonalInfo", "ResumeBuilder");
			}
			return View(model);
		}
		public IActionResult PersonalInfo(PersonalInfoViewModel model)
		{
			if (ModelState.IsValid)
			{
				_jsonPayloadString.PersonalInfoPayload = model;
				return RedirectToAction("Certificate", "ResumeBuilder");
			}
			return View(model);
		}
		public IActionResult Certificate(CertificateViewModel model)
		{
			if (ModelState.IsValid)
			{
				_jsonPayloadString.CertificatePayload = model;
				return RedirectToAction("Education", "ResumeBuilder");

			}
			return View(model);
		}

		public IActionResult Education(EducationViewModel model)
		{
			if (ModelState.IsValid)
			{
				_jsonPayloadString.EducationPayload = model;
                return RedirectToAction("Skill", "ResumeBuilder");

            }
            return View(model);
		}

        public IActionResult Skill(SkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                _jsonPayloadString.SkillPayload = model;
            }
            return View(model);
        }

    }
}
