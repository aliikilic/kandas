using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebUI.Dtos.UserDto;

namespace WebUI.Controllers
{
    public class InquiryformController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InquiryformController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(CreateInquiryFormDto dto)
        {
            var personId = HttpContext.Session.GetInt32("personId");
            var client = _httpClientFactory.CreateClient();
            dto.PersonalInformationId = Convert.ToInt32(personId);
            dto.LastCheckDate = DateTime.Now;
            var data = JsonConvert.SerializeObject(dto);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PostAsync($"https://localhost:7208/api/inquiry/InquiryForm?id={personId}", stringContent);
            return View();

        }
    }
}
