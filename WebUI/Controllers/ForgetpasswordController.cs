using Entities.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Contracts;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using WebUI.Dtos.MemberDto;

namespace WebUI.Controllers
{
    public class ForgetpasswordController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private int confirmKey;
        private TimeSpan createTime, lastTime;
        private string createTimeString,lastTimeString;
        
        public ForgetpasswordController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("deneme")]
        public async Task<IActionResult> SendMail(ResetPasswordDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            MailModel model = new()
            {
                To = dto.Email
            };


            var jsonData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://localhost:7208/api/mail/", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string,object>>(jsonResponse);

            TempData["confirmKey"]= Convert.ToInt32(jsonObject["key"]);
            TempData["createTimeString"] = jsonObject["createTime"].ToString();
            TempData["lastTimeString"] = jsonObject["gecerlilik"].ToString();
            TempData["email"] = dto.Email;
            


            return RedirectToAction("ResetPassword", "Forgetpassword");
        }
        [HttpGet]
        public PartialViewResult ResetPassword()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            
            int ck = Convert.ToInt32(TempData["confirmKey"]);
            string createTimeString = Convert.ToString(TempData["createTimeString"]);
            TimeSpan.TryParse(createTimeString, out createTime);
            string lastTimeString = Convert.ToString(TempData["lastTimeString"]);
            TimeSpan.TryParse(lastTimeString, out lastTime);
            var result = CheckCodeandExpire(resetPasswordDto.ConfirmKey, ck,createTime,lastTime);

            if (result == false)
                return BadRequest();
            
            var client = _httpClientFactory.CreateClient();

            UserForAuthenticationDto model = new()
            {
                Email = Convert.ToString(TempData["email"]),
                //Email = resetPasswordDto.Email,
                Password = resetPasswordDto.Password,
            };


            var jsonData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PostAsync("https://localhost:7208/api/member/ResetPassword/", content);

            //return true;
            return RedirectToAction("Index", "Login");

        }

        private bool CheckCodeandExpire(int? key, int confirmKey, TimeSpan date,TimeSpan time)
        {

            if (!(key != confirmKey && time.TotalSeconds <= date.TotalSeconds))
            {
                return false;
            }
            return true;

        }
       





    }
}
