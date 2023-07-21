using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs;
using Online_CV_Builder.Services;

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
