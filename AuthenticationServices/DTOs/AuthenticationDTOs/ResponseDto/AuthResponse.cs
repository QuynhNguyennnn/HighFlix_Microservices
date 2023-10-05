namespace APIS.DTOs.ResponseDto
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

    }
}
