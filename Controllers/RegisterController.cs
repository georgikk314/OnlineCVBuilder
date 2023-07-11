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
using Online_CV_Builder.DTOs;

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
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            try
            {
                if (_ResumeBuilderContext.Users.Any(u => u.Email == user.Email))
                {
                    return BadRequest("A user with this email already exists");
                }
                CreatePasswordHash(user.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                // RegisterDTO NewUser = new RegisterDTO();
                Users newUser = new Users();
                newUser.Email = user.Email;
                newUser.Username = user.Username;
                newUser.Password = user.Password;
                newUser.PasswordHash = PasswordHash;
                newUser.PasswordSalt = PasswordSalt;
                newUser.VerificationToken = CreateRandomToken();
                _ResumeBuilderContext.Users.Add(newUser);
                await _ResumeBuilderContext.SaveChangesAsync();
                return Ok("Registration successful");
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
                throw;
            }
        }
        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}