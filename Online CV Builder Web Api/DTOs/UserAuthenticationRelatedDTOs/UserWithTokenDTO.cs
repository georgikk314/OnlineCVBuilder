namespace Online_CV_Builder.DTOs.UserAuthenticationRelatedDTOs
{
    public class UserWithTokenDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
