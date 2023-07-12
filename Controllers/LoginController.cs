using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.DTOs;
using Online_CV_Builder.Services;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_CV_Builder.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthService;
        public LoginController(IUserAuthenticationService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDto)
        {
            var userWithToken = await _userAuthService.AuthenticateAsync(userDto);

            if (userWithToken == null)
                return Unauthorized();

            return Ok(userWithToken);
        }
        
    }
}
