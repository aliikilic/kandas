namespace WebUI.Dtos.MemberDto
{
    public record UserForAuthenticationDto
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}
