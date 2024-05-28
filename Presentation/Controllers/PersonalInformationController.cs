using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonalInformationController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public PersonalInformationController(IServiceManager manager)
        {
            _manager = manager;
        }
        
        [HttpGet("GetAllPersons")]
        public async Task<IActionResult> GetAllPersonsAsync()
        {
            return Ok(await _manager.PersonalInformationService.GetAllPersonsAsync(false));
        }

        [HttpGet("GetOnePerson")]
        public async Task<IActionResult> GetOnePersonAsync(int id)
        {
            return Ok(await _manager.PersonalInformationService.GetOnePersonAsync(id, false));
        }

        [HttpPost("CreatePerson")]
        public async Task<IActionResult> CreatePerson(PersonDto person,string email)
        {
            await _manager.PersonalInformationService.CreateOnePerson(person,email);
            return StatusCode(201);
        }
        [HttpGet("GetPIID")]
        public async Task<IActionResult> GetPersonId(string userid)
        {
            return Ok(await _manager.PersonalInformationService.GetPersonByUserId(userid));
        }
    }
}
