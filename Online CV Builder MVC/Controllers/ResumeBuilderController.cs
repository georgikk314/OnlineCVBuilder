using Abp.Collections.Extensions;
using Abp.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol;
using Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs;
using Online_CV_Builder_MVC.JsonPayload;
using Online_CV_Builder_MVC.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;

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
		private HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5096/api"; // API base URL

        public ResumeBuilderController(JsonPayloadString jsonPayloadString, IMemoryCache skillCache, IMemoryCache languageCache, IMemoryCache educationCache, IMemoryCache workExperienceCache, IMemoryCache certificateCache, IMemoryCache cache, HttpClient httpClient)
        {
			_jsonPayloadString = jsonPayloadString;
			_skillCache = skillCache;
			_languageCache = languageCache;
			_educationCache = educationCache;
			_workExperienceCache = workExperienceCache;
			_certificateCache = certificateCache;
			_cache = cache;
			_httpClient = httpClient;
        }
        public IActionResult ResumeTitle(ResumeTitleViewModel model) 
		{
			if(ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(4096);
				_cache.Set<ResumeTitleViewModel>("resumeTitle", model, cacheEntryOptions);
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
					.SetSize(4096);
				_cache.Set<PersonalInfoViewModel>("personalInfo", model, cacheEntryOptions);
				return RedirectToAction("Location", "ResumeBuilder");
			}
			return View(model);
		}

		public IActionResult Location(LocationViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(600))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
					.SetPriority(CacheItemPriority.Normal)
					.SetSize(4096);
				List<LocationViewModel> temp = new List<LocationViewModel>();
				temp.Add(model);
				_cache.Set<List<LocationViewModel>>("locations", temp, cacheEntryOptions);
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
					.SetSize(4096);
			var workExperience = _workExperienceCache.Get<WorkExperienceViewModel>("workExperience");
			if (!_cache.Get<List<WorkExperienceViewModel>>("workExperiences").IsNullOrEmpty())
			{
				List<WorkExperienceViewModel> temp = new List<WorkExperienceViewModel>();
				foreach (var item in _cache.Get<List<WorkExperienceViewModel>>("workExperiences"))
				{
					temp.Add(item);
				}
				temp.Add(workExperience);
				_cache.Set<List<WorkExperienceViewModel>>("workExperiences", temp, cacheEntryOptions);
			}
			else if (workExperience != null)
			{
				List<WorkExperienceViewModel> temp = new List<WorkExperienceViewModel>();
				temp.Add(workExperience);
				_cache.Set<List<WorkExperienceViewModel>>("workExperiences", temp, cacheEntryOptions);
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
					.SetSize(4096);
				_workExperienceCache.Set<WorkExperienceViewModel>("workExperience", model, cacheEntryOptions);
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
					.SetSize(4096);
			var education = _educationCache.Get<EducationViewModel>("education");
			if (!_cache.Get<List<EducationViewModel>>("educations").IsNullOrEmpty())
			{
				List<EducationViewModel> temp = new List<EducationViewModel>();
				foreach (var item in _cache.Get<List<EducationViewModel>>("educations"))
				{
					temp.Add(item);
				}
				temp.Add(education);
				_cache.Set<List<EducationViewModel>>("educations", temp, cacheEntryOptions);
			}
			else if (education != null)
			{
				List<EducationViewModel> temp = new List<EducationViewModel>();
				temp.Add(education);
				_cache.Set<List<EducationViewModel>>("educations", temp, cacheEntryOptions);
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
					.SetSize(4096);
				_educationCache.Set<EducationViewModel>("education", model, cacheEntryOptions);
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
					.SetSize(4096);
			var skill = _skillCache.Get<SkillViewModel>("skill");
			if(!_cache.Get<List<SkillViewModel>>("skills").IsNullOrEmpty())
			{
				List<SkillViewModel> temp = new List<SkillViewModel>();
				foreach (var item in _cache.Get<List<SkillViewModel>>("skills"))
				{
					temp.Add(item);
				}
				temp.Add(skill);
				_cache.Set<List<SkillViewModel>>("skills", temp, cacheEntryOptions);
			}
			else if(skill != null)
			{
				List<SkillViewModel> temp = new List<SkillViewModel>();
				temp.Add(skill);
				_cache.Set<List<SkillViewModel>>("skills", temp, cacheEntryOptions);
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
					.SetSize(4096);
				_skillCache.Set<SkillViewModel>("skill", model, cacheEntryOptions);
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
					.SetSize(4096);
			var certificate = _certificateCache.Get<CertificateViewModel>("certificate");
			if (!_cache.Get<List<CertificateViewModel>>("certificates").IsNullOrEmpty())
			{
				List<CertificateViewModel> temp = new List<CertificateViewModel>();
				foreach (var item in _cache.Get<List<CertificateViewModel>>("certificates"))
				{
					temp.Add(item);
				}
				temp.Add(certificate);
				_cache.Set<List<CertificateViewModel>>("certificates", temp, cacheEntryOptions);
			}
			else if (certificate != null)
			{
				List<CertificateViewModel> temp = new List<CertificateViewModel>();
				temp.Add(certificate);
				_cache.Set<List<CertificateViewModel>>("certificates", temp, cacheEntryOptions);
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
					.SetSize(4096);
				_certificateCache.Set<CertificateViewModel>("certificate", model, cacheEntryOptions);
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
					.SetSize(4096);
			var language = _languageCache.Get<LanguageViewModel>("language");
			if (!_cache.Get<List<LanguageViewModel>>("languages").IsNullOrEmpty())
			{
				List<LanguageViewModel> temp = new List<LanguageViewModel>();
				foreach (var item in _cache.Get<List<LanguageViewModel>>("languages"))
				{
					temp.Add(item);
				}
				temp.Add(language);
				_cache.Set("languages", temp, cacheEntryOptions);
			}
			else if (language != null)
			{
				List<LanguageViewModel> temp = new List<LanguageViewModel>();
				temp.Add(language);
				_cache.Set<List<LanguageViewModel>>("languages", temp, cacheEntryOptions);
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
					.SetSize(4096);
				_languageCache.Set<LanguageViewModel>("language", model, cacheEntryOptions);
				return RedirectToAction("Languages", "ResumeBuilder");
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ResumeSave()
		{
			var resumeTitle = _cache.Get<ResumeTitleViewModel>("resumeTitle");
			var personalInfo = _cache.Get<PersonalInfoViewModel>("personalInfo");
			var locations = _cache.Get<List<LocationViewModel>>("locations");
			var workExperiences = _cache.Get<List<WorkExperienceViewModel>>("workExperiences");
			var educations = _cache.Get<List<EducationViewModel>>("educations");
			var skills = _cache.Get<List<SkillViewModel>>("skills");
			var certificates = _cache.Get<List<CertificateViewModel>>("certificates");
			var languages = _cache.Get<List<LanguageViewModel>>("languages");

			var requestBody = new
			{
				Title = resumeTitle.ResumeTitle,
				PersonalInfo = personalInfo,
				Locations = locations,
				WorkExperiences = workExperiences,
				Educations = educations,
				Skills = skills,
				Certificates = certificates,
				Languages = languages
			};

			var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Send the POST request to the API endpoint
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/resumes", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                ModelState.AddModelError("errRes", "Error with saving the resume!");
            }
            else
            {
                ModelState.AddModelError("err", "An enexpected error occurred!");
            }

			return RedirectToAction("Index", "Home");
        }
	}
}
