using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Models;

namespace Online_CV_Builder.Controllers
{
    [Route("api/verify")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public VerifyController(ResumeBuilderContext ResumeBuilderContext)
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyDTO user3)
        {
            var user4 = await _ResumeBuilderContext.Users.FirstOrDefaultAsync(u => u.VerificationToken == user3.VerificationToken);
            if (user4 == null) 
            {
                return BadRequest("Invalid Token");
            }
            user4.VerifiedAt = DateTime.Now;
            await _ResumeBuilderContext.SaveChangesAsync();
            return Ok("User verified");
        }
    }
}
