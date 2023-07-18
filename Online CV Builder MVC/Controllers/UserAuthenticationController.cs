using Microsoft.AspNetCore.Mvc;

namespace Online_CV_Builder_MVC.Controllers
{
    public class UserAuthenticationController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5096/api");
        private readonly HttpClient _client;

        public UserAuthenticationController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Login()
        {

            return View();
        }
    }
}
