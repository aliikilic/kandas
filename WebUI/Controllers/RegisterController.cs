using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using WebUI.Dtos.MemberDto;

namespace WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(UserManager<User> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registeration(UserRegisterationDto userRegisterationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(userRegisterationDto);
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            await client.PostAsync("https://localhost:7208/api/member/SignUp", stringContent);
            return RedirectToAction("index","Login");
        }



    }
}
