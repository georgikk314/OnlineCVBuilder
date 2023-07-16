using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Services;
using System.Security.Claims;

namespace Online_CV_Builder.Controllers
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthService;
        public LogoutController(IUserAuthenticationService userAuthService) 
        {
            _userAuthService = userAuthService;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = GetRefreshTokenFromClaims();

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Invalid refresh token");

            await _userAuthService.LogoutAsync(refreshToken);

            return Ok();
        }

        private string GetRefreshTokenFromClaims()
        {
            var refreshTokenClaim = HttpContext.User?.FindFirst("RefreshToken");

            if (refreshTokenClaim != null)
                return refreshTokenClaim.Value;

            return null;
        }
    }
}