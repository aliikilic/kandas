using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Dtos.UserDto;

namespace WebUI.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomePageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        

        public async Task<IActionResult> Index()
        {
            var personId = HttpContext.Session.GetInt32("personId");
            var client = _httpClientFactory.CreateClient();
            
            var responseMessage = await client.GetAsync($"https://localhost:7208/api/notification/GetPersonalNotificationList?id={personId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserNotificationDto>>(jsonData);
                
                return View(values);

            }
            else
                return View();
        }

       


    }
}
