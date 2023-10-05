namespace AuthenticationServices.DTOs.AuthenticationDTOs.ResponseDto
{
    public class UserResponse
    {
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string Email { get; set; } = null!;

        public string? RegistedDate { get; set; }

        public string? Avatar { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
