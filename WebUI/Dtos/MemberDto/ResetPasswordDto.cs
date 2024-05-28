using System.ComponentModel.DataAnnotations;

namespace WebUI.Dtos.MemberDto
{
    public class ResetPasswordDto
    {
        public string? Email { get; init; }
        public string? Password { get; set; }
        public int? ConfirmKey { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
    }
}
