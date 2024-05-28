using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebUI.Dtos.UserDto;
using WebUI.Models.Adress;

namespace WebUI.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonalInfoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessageCity = await client.GetAsync("https://localhost:7208/api/city/cities");
            var jsonDataCity = await responseMessageCity.Content.ReadAsStringAsync();
            var Cityvalues = JsonConvert.DeserializeObject<List<GetCitiesDto>>(jsonDataCity);

            var responseMessageDistricts = await client.GetAsync("https://localhost:7208/api/city/distircts");
            var jsonDataDistrict = await responseMessageDistricts.Content.ReadAsStringAsync();
            var Distictvalues = JsonConvert.DeserializeObject<List<GetDistrictsDto>>(jsonDataDistrict);

            var responseMessageNeighborhoods = await client.GetAsync("https://localhost:7208/api/city/neighborhoods");
            var jsonDataNeighborhood = await responseMessageNeighborhoods.Content.ReadAsStringAsync();
            var Neighborhoodvalues = JsonConvert.DeserializeObject<List<GetNeighborhoodsDto>>(jsonDataNeighborhood);


            ViewBag.Cities = JsonConvert.SerializeObject(Cityvalues);
            ViewBag.Districts = JsonConvert.SerializeObject(Distictvalues);
            ViewBag.Neighborhoods = JsonConvert.SerializeObject(Neighborhoodvalues);



            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateInformation(PersonalInfoDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            dto.FatherName = "ADEM";
            dto.BirthPlaceId = 34;
            dto.IsActive = true;
            var data = JsonConvert.SerializeObject(dto);

            var email= HttpContext.Session.GetString("email");

            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PostAsync($"https://localhost:7208/api/persons/CreatePerson?email={email}", stringContent);


            return View(dto);
        }
        
        
    }
}
