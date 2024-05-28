using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Contracts;
using Repositories.EfCore;
using Services.Contracts;
using System.Text;
using WebUI.Dtos.AdminDto;

namespace WebUI.Controllers
{
    public class AdminPagesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminPagesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7208/api/city/cities");

                List<AdminCityDto> DropDownListItems = new List<AdminCityDto>();
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AdminCityDto>>(jsonData);



                
                foreach (var item in values)
                {
                    DropDownListItems.Add(new AdminCityDto
                    {
                        CityID = item.CityID, 
                        CityName = item.CityName
                        
                    });
                }
                ViewBag.Cities = DropDownListItems;
            }

            var response = await client.GetAsync("https://localhost:7208/api/movements/GetHospitalList");
            if (response.IsSuccessStatusCode)
            {

                var data = await response.Content.ReadAsStringAsync();
                var hospitals = JsonConvert.DeserializeObject<List<AdminHospitalsDto>>(data);

                List<AdminHospitalsDto> hospitalList = new List<AdminHospitalsDto>();
                foreach (var item in hospitals)
                {
                    hospitalList.Add(new AdminHospitalsDto
                    {
                        ID = item.ID,
                        HospitalName = item.HospitalName
                    });
                }
                ViewBag.HospitalList = hospitalList;
            }
            return View();
        }
    

        [HttpGet]
        public PartialViewResult AddRecepient()
        {
           return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecepient(AdminRecepientDto dto)
        {
            //
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(data,Encoding.UTF8,"application/json");
            await client.PostAsync("https://localhost:7208/api/recepient/NewRecepient",stringContent);
            return RedirectToAction("index","AdminPages");
        }
        [HttpGet]
        public PartialViewResult AddDonation()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDonation(AdminDonationDto dto)
        {
            var tckn = Request.Form["tckn"];
            var client = _httpClientFactory.CreateClient();
            
            
            var responseMessage = await client.GetAsync($"https://localhost:7208/api/recepient/GetRecepientIdByTCKN?tckn={tckn}");

            var recepId = await responseMessage.Content.ReadAsStringAsync();
            dto.RecepientId = Convert.ToInt32(recepId);
            dto.IsActive =true;
            var data = JsonConvert.SerializeObject(dto);
            
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7208/api/donations/GenerateDonation", stringContent);
            return RedirectToAction("index", "AdminPages");

            
        }
    }
}
