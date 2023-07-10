using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Data;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_CV_Builder.Controllers
{
    [Route("api/login")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public LoginController(ResumeBuilderContext ResumeBuilderContext)
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO user1)
        {
            var user2 = await _ResumeBuilderContext.Users.FirstOrDefaultAsync(u => u.Email == user1.Email);
            if (user2 == null)
            {
                return BadRequest("User not found");
            }
            if (user2.VerifiedAt == null)
            {
                return BadRequest("User not verified");
            }
            if (!VerifyPasswordHash(user1.Password, user2.PasswordHash, user2.PasswordSalt))
            {
                return BadRequest("Password is incorect");
            }
            return Ok($"Welcome back, {user2.Email} !  :-) ");
        }
        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt) 
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedhash.SequenceEqual(passwordhash);
            }
        }
    }
}

