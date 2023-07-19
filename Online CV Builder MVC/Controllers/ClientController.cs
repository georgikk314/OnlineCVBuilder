using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder_MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Online_CV_Builder_MVC.Controllers
{
    public class ClientController : Controller
    {
        string baseUrl = "http://localhost:5187";
        List<ClientViewModel> ClientInfo = new List<ClientViewModel>();
        public IActionResult ToResume()
        {
            return View();
        }
        public async Task<IActionResult> ToResumes()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.GetAsync("api/resumes");
                if (response.IsSuccessStatusCode)
                {
                    var EmpResponse = response.Content.ReadAsStringAsync().Result;
                    // ClientInfo = JsonConvert.DeserializeObject<List<Client>>(EmpResponse);
                    return View(EmpResponse);
                }
                return View(null);
            }
        }
        public async Task<IActionResult> ToResumeId()
        {
            List<ClientViewModel> ClientInfo = new List<ClientViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                //HttpResponseMessage Res = await client.GetAsync("api/resumes/view/{resumeTemplateId}");
                var responseResume = await client.GetAsync("api/resumes/view/{resumeTemplateId}");
                if (responseResume.IsSuccessStatusCode)
                {
                    var resumeFile = responseResume.Content;
                    return View("ToResume");
                }
                return View();
            }
        }
        public async Task<IActionResult> GetAllResumesAsync() 
        {
            return View();
        }
        public IActionResult Client()
        {
            return View();
        }
        public IActionResult ToBuilder() 
        {
            return View();
        }
    }
}
