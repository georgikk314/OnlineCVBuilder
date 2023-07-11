using Online_CV_Builder.DTOs;
using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Services
{
    public interface IUserAuthenticationService
    {
        public Task<Users> RegisterAsync(RegisterDTO registerDto);
        public Task<Users> AuthenticateAsync(UserDTO userDto);
    }
}
