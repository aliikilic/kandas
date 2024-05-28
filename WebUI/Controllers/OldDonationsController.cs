using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebUI.Dtos.UserDto;

namespace WebUI.Controllers
{
    public class OldDonationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OldDonationsController(IHttpClientFactory httpClientFactory)
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

                var responseMessageP = await client.GetAsync($"https://localhost:7208/api/movements/GetMovementsByPersonId?id={personId}");
                if (responseMessageP.IsSuccessStatusCode)
                {
                    var jsonDataP = await responseMessageP.Content.ReadAsStringAsync();
                    var valuesP = JsonConvert.DeserializeObject<List<PreviousDonationDto>>(jsonDataP);

                    ViewData["previousDonations"] = valuesP;


                    return View(values);
                }
                return View();
            }
            else
                return View();
            
        }

        
    }
}
