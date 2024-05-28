using Entities.Dtos;
using Entities.Models;
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
    [Route("api/recepient")]
    public class RecepientsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public RecepientsController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpPost("NewRecepient")]
        public async Task<IActionResult> CreateNewRecepient(CreateRecepientDto recepient)
        {
            await _manager.ReceientService.CreateRecepient(recepient);
            return Ok();
        }
        [HttpGet("GetRecepients")]
        public async Task<IActionResult> GetAllRecepients()
        {
            var entity = await _manager.ReceientService.GetAllRecepients(false);
            return Ok(entity);
        }
        [HttpGet("GetRecepientIdByTCKN")]
        public async Task<IActionResult> GetRecepientIdByTCKN(string tckn)
        {
            var id = await _manager.ReceientService.GetRecepientIdByTCKN(tckn);
            return Ok(id);
        }
    }
}
