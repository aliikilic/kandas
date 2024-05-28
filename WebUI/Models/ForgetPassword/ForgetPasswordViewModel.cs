using Entities.Dtos;
using WebUI.Dtos.MailDto;

namespace WebUI.Models.ForgetPassword
{
    public class ForgetPasswordViewModel
    {
        public ForgetPassMailDto forgetPassMailDto { get; set; }
        public UserForAuthenticationDto userForAuthenticationDto { get; set; }
    }
}
