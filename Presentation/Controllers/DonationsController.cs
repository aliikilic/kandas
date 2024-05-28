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
    [Route("api/donations")]
    public class DonationsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public DonationsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllDonations")]
        public async Task<IActionResult> GetAllDonations()
        {
            var list = await _manager.DonationService.GetAllDonation(false);
            return Ok(list);
        }
        [HttpGet("GetOneDonationByRecepientId")]
        public async Task<IActionResult> GetOneDonation(int id)
        {
            var entity = await _manager.DonationService.GetDonationByRecepientId(id, false);
            return Ok(entity);
        }

        [HttpPost("GenerateDonation")]
        public async Task<IActionResult> GenerateDonation(CreateDonationDto donation)
        {
            await _manager.DonationService.CreateDonation(donation);
            return NoContent();
        }
    }
}
