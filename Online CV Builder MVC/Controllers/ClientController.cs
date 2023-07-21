using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder_MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json;

namespace Online_CV_Builder_MVC.Controllers
{
    public class ClientController : Controller
    {
		private HttpClient _httpClient;
		private readonly string _apiBaseUrl = "http://localhost:5096/api"; // API base URL

        public ClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

		public async Task<IActionResult> ToResumes(IEnumerable<ResumeViewModel> model)
        {

            //if(User.FindFirstValue(ClaimTypes.NameIdentifier) == null) return Unauthorized(ModelState);
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 2;

			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/resumes/userView/{userId}");

			// Ensure the response is successful
			response.EnsureSuccessStatusCode();

			// Read the JSON content from the response
			string jsonData = await response.Content.ReadAsStringAsync();

			var resumes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResumeViewModel>>(jsonData);
            model = resumes;

			return View(model);
        }

        public IActionResult ResumeSharing()
        {
            return RedirectToAction("Share", "Client");
        }

        public IActionResult ResumeDownloading()
        {
            return RedirectToAction("Download", "Client");
        }

        public IActionResult Share()
        {
            return View(new ShareViewModel());
        }

        public IActionResult Download()
        {
            return View(new DownloadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ShareWith(ShareViewModel model) 
        { 
            if(ModelState.IsValid)
            {

                var requestBody = new
                {
                    ResumeTitle = model.ResumeTitle,
                    RecipientEmail = model.RecipientEmail,
                    Message = "I present you my resume!"
                };

				string jsonBody = JsonSerializer.Serialize(requestBody);
				var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

				// Send the POST request to the API endpoint
				var response = await _httpClient.PostAsync($"{_apiBaseUrl}/sharing", content);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("ResumeTo", "Client");
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
				{
					ModelState.AddModelError("errRes", "Error with sharing the resume!");
				}
				else
				{
					ModelState.AddModelError("err", "An enexpected error occurred!");
				}
			}
            return View(model);
        }

        public async Task<IActionResult> Downloading(DownloadViewModel model)
        {
			if (ModelState.IsValid)
			{
				
				var response = await _httpClient.GetAsync($"{_apiBaseUrl}/download/{model.ResumeId}");

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("ResumeTo", "Client");
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
				{
					ModelState.AddModelError("errRes", "Error with downloading the resume!");
				}
				else
				{
					ModelState.AddModelError("err", "An enexpected error occurred!");
				}
			}
			return View(model);
		}
    }
}
