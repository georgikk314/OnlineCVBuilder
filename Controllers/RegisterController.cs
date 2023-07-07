using Abp.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;
using Online_CV_Builder.Configuration;
using System.Text.Json.Nodes;
using Online_CV_Builder.Models;
using System.Security.Cryptography;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Online_CV_Builder.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public RegisterController(ResumeBuilderContext ResumeBuilderContext) 
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user1) 
        {
            if (_ResumeBuilderContext.Users.Any(u => u.Email == user1.Email)) 
            {
                return BadRequest("A user with this email already exists");
            }
            CreatePasswordHash(user1.Password, out byte[] passwordhash, out byte[] passwordsalt);
            Users newuser = new Users();
            newuser.Email = user1.Email;
            newuser.Username = user1.Username;
            newuser.Password = user1.Password;
            newuser.PasswordHash = passwordhash;
            newuser.PasswordSalt = passwordsalt;
            newuser.VerificationToken = CreateRandomToken();
            _ResumeBuilderContext.Users.Add(newuser);
            await _ResumeBuilderContext.SaveChangesAsync();
            return Ok("Registration successful");
        }
        private void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512()) 
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateRandomToken() 
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
