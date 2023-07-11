using Microsoft.AspNetCore.Mvc;

namespace Online_CV_Builder.Controllers
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Logout()
        {
            // Perform logout operations here (e.g., clear session, invalidate tokens)

            // Redirect user to home page
            return RedirectToAction("LoggedOut", "Home");
        }
    }
}