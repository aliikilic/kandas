using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/movements")]
    public class DonationMovementsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public DonationMovementsController(IServiceManager manager)
        {
            _manager = manager;
        }
                
        [HttpGet("GetAllMovements")]
        public async Task<IActionResult> GetAllDonationMovementsAsync()
        {
            var entity = await _manager.DonationMovementService.GetDonationMovementsAsync(false);
            return Ok(entity);
        }
                
        [HttpGet("GetMovementsByPersonId")]
        public async Task<IActionResult> GetMovementsByPersonIdAsync(int id)
        {
            var movements = await _manager.DonationMovementService.GetDonationMovementsByPersonId(id,false);

            return Ok(movements);
        }

        [HttpGet("GetOneMovementById")]
        public async Task<IActionResult> GetOneMovementById(int id)
        {
            var movement = _manager.DonationMovementService.GetOneDonationMovementById(id,false);
            return Ok(movement);
        }

        [HttpGet("CreateMovement")]
        public async Task<IActionResult> CreateMovement()
        {
             await _manager.DonationMovementService.CreteDonationMovement();

            //if(!(entity.IsCompletedSuccessfully))
            //    return BadRequest();
            return NoContent();
        }
        [HttpPut("UpdateMovementStatus")]
        public async Task<IActionResult> UpdateMovementStatus(int id , int statusId)
        {
            await _manager.DonationMovementService.UpdateDonationMovementStatus(id,statusId,true);
            return NoContent();
        }
        [HttpGet("GetHospitalList")]
        public IActionResult GetHospitalsList()
        {
            var entity = _manager.DonationMovementService.GetHospitalList();
            return Ok(entity);
        }
    }
}
