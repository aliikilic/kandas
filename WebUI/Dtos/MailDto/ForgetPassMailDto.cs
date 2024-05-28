namespace WebUI.Dtos.MailDto
{
    public class ForgetPassMailDto
    {
        public string? To { get; init; }
        public string? From { get; } = "kandasdestek@gmail.com";
        public string? Subject { get; init; }
        public string? Body { get; init; }
        public DateTime SendTime { get; init; } = DateTime.Now;
    }
}
