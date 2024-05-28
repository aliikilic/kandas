using Entities.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebUI.Dtos;
using WebUI.Dtos.MemberDto;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public LoginController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(userForAuthenticationDto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMessage = await client.PostAsync("https://localhost:7208/api/member/Login/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<UiTokenDto>(responseContent);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(responseData.AccesToken) as JwtSecurityToken;
                
                if(jwtToken != null)
                {
                    var idClaim = jwtToken.Claims.FirstOrDefault(c=> c.Type==ClaimTypes.Sid)?.Value;
                    var roleClaim = jwtToken.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.Role)?.Value;

                    var responseId = await client.GetAsync($"https://localhost:7208/api/persons/GetPIID?userid={idClaim}");
                    var person = await responseId.Content.ReadAsStringAsync();
                    int personId= Convert.ToInt32(person);
                    HttpContext.Session.SetInt32("personId",personId);
                    HttpContext.Session.SetString("email",userForAuthenticationDto.Email);
                    if (roleClaim == "Admin")
                    {
                        return RedirectToAction("Index", "AdminPages");
                    }
                    else
                        return RedirectToAction("Index","HomePage");
                }
                return RedirectToAction("Index","Register");
            }
            return View();
        }
    }
}
