using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs;
using System.Security.Cryptography;

namespace Online_CV_Builder.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly ResumeBuilderContext _dbContext;
        public UserAuthenticationService(ResumeBuilderContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Users> RegisterAsync(RegisterDTO registerDto)
        {
            // Validate the input
            if (registerDto == null)
            {
                throw new ArgumentNullException(nameof(registerDto));
            }

            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            // Check if the username or email is already taken
            if (await _dbContext.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                throw new ArgumentException("Username is already taken.");
            }

            if (await _dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new ArgumentException("Email is already taken.");
            }

            // Create a new user entity
            var user = new Users
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
            };

            // Generate password hash and salt
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Save the new user to the database
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
