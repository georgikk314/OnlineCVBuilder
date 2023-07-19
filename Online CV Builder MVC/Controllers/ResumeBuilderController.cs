using Abp.Collections.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Online_CV_Builder_MVC.JsonPayload;
using Online_CV_Builder_MVC.Models;

namespace Online_CV_Builder_MVC.Controllers
{
	public class ResumeBuilderController : Controller
	{
		private JsonPayloadString _jsonPayloadString;
		private IMemoryCache _cache;
		private IMemoryCache _skillCache;
		private IMemoryCache _languageCache;
		private IMemoryCache _educationCache;
		private IMemoryCache _workExperienceCache;
		private IMemoryCache _certificateCache;

		public ResumeBuilderController(JsonPayloadString jsonPayloadString, IMemoryCache skillCache, IMemoryCache languageCache, IMemoryCache educationCache, IMemoryCache workExperienceCache, IMemoryCache certificateCache, IMemoryCache cache)
        {
			_jsonPayloadString = jsonPayloadString;
			_skillCache = skillCache;
			_languageCache = languageCache;
			_educationCache = educationCache;
			_workExperienceCache = workExperienceCache;
			_certificateCache = certificateCache;
			_cache = cache;
        }
        public IActionResult ResumeTitle(ResumeTitleViewModel model) 
		{
			if(ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_cache.Set("resumeTitle", model, cacheEntryOptions);
				return RedirectToAction("PersonalInfo", "ResumeBuilder");
			}
			return View(model);
		}
		public IActionResult PersonalInfo(PersonalInfoViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_cache.Set("personalInfo", model, cacheEntryOptions);
				return RedirectToAction("WorkExperiences", "ResumeBuilder");
			}
			return View(model);
		}
		
		public IActionResult WorkExperiences(IEnumerable<WorkExperienceViewModel> model)
		{

			var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
			var workExperience = _workExperienceCache.Get("workExperience");
			if (!_cache.Get<List<object>>("workExperiences").IsNullOrEmpty())
			{
				List<object> temp = new List<object>();
				foreach (var item in _cache.Get<List<object>>("workExperiences"))
				{
					temp.Add(item);
				}
				temp.Add(workExperience);
				_cache.Set("workExperiences", temp, cacheEntryOptions);
			}
			else if (workExperience != null)
			{
				List<object> temp = new List<object>();
				temp.Add(workExperience);
				_cache.Set("workExperiences", temp, cacheEntryOptions);
			}

			//model = (List<SkillViewModel>)_cache.Get("skills");
			return View(model);
		}

		public IActionResult CreateWorkExperience(WorkExperienceViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_workExperienceCache.Set("workExperience", model, cacheEntryOptions);
				return RedirectToAction("WorkExperiences", "ResumeBuilder");
			}
			return View(model);
		}


        public IActionResult Education(IEnumerable<EducationViewModel> model)
        {
			var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
			var education = _educationCache.Get("education");
			if (!_cache.Get<List<object>>("educations").IsNullOrEmpty())
			{
				List<object> temp = new List<object>();
				foreach (var item in _cache.Get<List<object>>("educations"))
				{
					temp.Add(item);
				}
				temp.Add(education);
				_cache.Set("educations", temp, cacheEntryOptions);
			}
			else if (education != null)
			{
				List<object> temp = new List<object>();
				temp.Add(education);
				_cache.Set("educations", temp, cacheEntryOptions);
			}

			//model = (List<SkillViewModel>)_cache.Get("skills");
			return View(model);
		}

		public IActionResult CreateEducation(EducationViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_educationCache.Set("education", model, cacheEntryOptions);
				return RedirectToAction("Education", "ResumeBuilder");
			}
			return View(model);
		}

        public IActionResult Skills(IEnumerable<SkillViewModel> model)
        {
			var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
			var skill = _skillCache.Get("skill");
			if(!_cache.Get<List<object>>("skills").IsNullOrEmpty())
			{
				List<object> temp = new List<object>();
				foreach (var item in _cache.Get<List<object>>("skills"))
				{
					temp.Add(item);
				}
				temp.Add(skill);
				_cache.Set("skills", temp, cacheEntryOptions);
			}
			else if(skill != null)
			{
				List<object> temp = new List<object>();
				temp.Add(skill);
				_cache.Set("skills", temp, cacheEntryOptions);
			}
			
			//model = (List<SkillViewModel>)_cache.Get("skills");
            return View(model);
        }

		public IActionResult CreateSkill(SkillViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_skillCache.Set("skill", model, cacheEntryOptions);
				return RedirectToAction("Skills", "ResumeBuilder");
			}
			return View(model);
		}

        public IActionResult Certificate(IEnumerable<CertificateViewModel> model)
		{
			var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
			var skill = _certificateCache.Get("certificate");
			if (!_cache.Get<List<object>>("certificates").IsNullOrEmpty())
			{
				List<object> temp = new List<object>();
				foreach (var item in _cache.Get<List<object>>("certificates"))
				{
					temp.Add(item);
				}
				temp.Add(skill);
				_cache.Set("certificates", temp, cacheEntryOptions);
			}
			else if (skill != null)
			{
				List<object> temp = new List<object>();
				temp.Add(skill);
				_cache.Set("certificates", temp, cacheEntryOptions);
			}

			//model = (List<SkillViewModel>)_cache.Get("skills");
			return View(model);
		}

		public IActionResult CreateCertificate(CertificateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_certificateCache.Set("certificate", model, cacheEntryOptions);
				return RedirectToAction("Certificate", "ResumeBuilder");
			}
			return View(model);
		}

		public IActionResult Languages(IEnumerable<LanguageViewModel> model)
		{

			var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
			var language = _languageCache.Get("language");
			if (!_cache.Get<List<object>>("languages").IsNullOrEmpty())
			{
				List<object> temp = new List<object>();
				foreach (var item in _cache.Get<List<object>>("languages"))
				{
					temp.Add(item);
				}
				temp.Add(language);
				_cache.Set("languages", temp, cacheEntryOptions);
			}
			else if (language != null)
			{
				List<object> temp = new List<object>();
				temp.Add(language);
				_cache.Set("languages", temp, cacheEntryOptions);
			}

			//model = (List<SkillViewModel>)_cache.Get("skills");
			return View(model);
		}

		public IActionResult CreateLanguage(LanguageViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(1024);
				_languageCache.Set("language", model, cacheEntryOptions);
				return RedirectToAction("Languages", "ResumeBuilder");
			}
			return View(model);
		}

	}
}
