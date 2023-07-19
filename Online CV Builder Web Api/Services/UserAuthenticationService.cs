using Google.Protobuf.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Online_CV_Builder.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly ResumeBuilderContext _dbContext;
        private readonly IConfiguration _configuration;
        //private readonly HttpContext _httpcontext;

        public UserAuthenticationService(ResumeBuilderContext dbContext, IConfiguration configuration/*, HttpContext httpContext*/)
        {
            _dbContext = dbContext;
            _configuration = configuration;
          //  _httpcontext = httpContext;
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

            // Generate refresh token
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            // Save the new user to the database
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserWithTokenDTO> AuthenticateAsync(UserDTO userDto)
        {
            var user = await _dbContext.Set<Users>().FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Generate refresh token
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            // Generate Jwt Token
            var token = GenerateJwtToken(user);

            var userWithToken = new UserWithTokenDTO()
            {
                Id = user.Id,
                Username = user.Username,
                Token = token,
                RefreshToken = refreshToken
            };

            return userWithToken;
        }

        public async Task<UserWithTokenDTO> RefreshTokenAsync(string refreshToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiration > DateTime.UtcNow);

            if (user == null)
                return null;

            // Generate new refresh token
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            // Generate new JWT token
            var newToken = GenerateJwtToken(user);

            var userWithToken = new UserWithTokenDTO()
            {
                Id = user.Id,
                Username = user.Username,
                Token = newToken,
                RefreshToken = newRefreshToken
            };

            return userWithToken;
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
                throw new ArgumentException("Invalid refresh token.");

            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;

            await _dbContext.SaveChangesAsync();
        }

        public string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration.GetValue<string>("Jwt:Secret");
            var audience = "online-cv-builder-app";
            var issuer = "your-application-domain.com";

            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT secret key is not configured.");
            }

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("RefreshToken", user.RefreshToken) // Include RefreshToken claim
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);
            /*
            _httpcontext.Response.Cookies.Append("token", encryptedToken, new CookieOptions { 
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
            */
            return encryptedToken;
        }

        /*
        public async Task<Users> LoginAsync(UserDTO userDto)
        {
            var user = await AuthenticateAsync(userDto.Username, userDto.Password);

            if (user == null)
                throw new Exception("Invalid username or password.");

            return user;
        }*/


        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string GenerateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[32];
                rngCryptoServiceProvider.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}

