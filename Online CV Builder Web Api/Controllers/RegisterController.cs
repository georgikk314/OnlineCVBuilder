using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs;
using Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs;
using Online_CV_Builder.Services;
using System.Security.Cryptography;

namespace Online_CV_Builder.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthService;
        public RegisterController(IUserAuthenticationService userAuthService)
        {
            _userAuthService = userAuthService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                var user = await _userAuthService.RegisterAsync(registerDto);
                var token = _userAuthService.GenerateJwtToken(user);
                var userWithToken = new UserWithTokenDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = token
                };
                return Ok(userWithToken);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error and return an appropriate response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering the user.");
            }
        }


    }
}