using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder_MVC.Models;
using System.Text.Json;
using System.Text;
using Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs;

namespace Online_CV_Builder_MVC.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5096/api"; // API base URL

        public UserAuthenticationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create the request body
                    var requestBody = new
                    {
                        Username = model.Username,
                        Password = model.Password
                    };

                    // Serialize the request body
                    var jsonBody = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send the POST request to the API endpoint
                    var response = await _httpClient.PostAsync($"{_apiBaseUrl}/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        var responseContent = await response.Content.ReadAsStringAsync();

                        // Deserialize the response JSON
                        var userWithToken = JsonSerializer.Deserialize<UserWithTokenDTO>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        // Store the token in a secure location (e.g., session, cookie, or local storage)
                        // Redirect to the desired page
                        return RedirectToAction("Index", "Home");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ModelState.AddModelError("wngCred", "Invalid username or password");
                    }
                    else
                    {
                        ModelState.AddModelError("errAuth", "An error occurred while authenticating");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("err", $"An error occurred: {ex.Message}");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create the request body
                    var requestBody = new
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword
                    };

                    // Serialize the request body
                    var jsonBody = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send the POST request to the API endpoint
                    var response = await _httpClient.PostAsync($"{_apiBaseUrl}/register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        var responseContent = await response.Content.ReadAsStringAsync();

                        // Store the token in a secure location (e.g., session, cookie, or local storage)
                        // Redirect to the desired page
                        return RedirectToAction("Login", "UserAuthentication");
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("errVal", "There is already a user with such username or password!");
                    }
                    else
                    {
                        ModelState.AddModelError("errReg", "An error occured while trying to register!");
                    }
                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("err", $"An error occurred: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/logout");
            return RedirectToAction("Index", "Home");
        }
    }
}
