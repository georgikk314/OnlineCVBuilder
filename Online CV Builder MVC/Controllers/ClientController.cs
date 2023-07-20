using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder_MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

			var resumes = JsonConvert.DeserializeObject<List<ResumeViewModel>>(jsonData);
            model = resumes;

			return View(model);
        }
    }
}
