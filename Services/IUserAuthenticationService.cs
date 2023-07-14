﻿using Online_CV_Builder.DTOs;
using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Services
{
    public interface IUserAuthenticationService
    {
        public Task<Users> RegisterAsync(RegisterDTO registerDto);
        public Task<UserWithTokenDTO> AuthenticateAsync(UserDTO userDto);
        public Task<UserWithTokenDTO> RefreshTokenAsync(string refreshToken);
        public Task LogoutAsync(string refreshToken);
        public string GenerateJwtToken(Users user);
    }
}
