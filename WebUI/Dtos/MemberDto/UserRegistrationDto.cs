using System.ComponentModel.DataAnnotations;

namespace WebUI.Dtos.MemberDto
{
    public record UserRegisterationDto
    {
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Password { get; init; }

        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string? ConfirmPassword { get; init; }
    }
}
