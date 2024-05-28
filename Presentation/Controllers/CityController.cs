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
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public CityController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities() 
        {
            return Ok(await _manager.CityService.GetCities(false));
        }
        [HttpGet("distircts")]
        public async Task<IActionResult> Getdistricts()
        {
            return Ok(await _manager.DistrictService.GetDistrictsAsync(false));
        }
        [HttpGet("neighborhoods")]
        public async Task<IActionResult> GetNeighborhoods()
        {
            var values = await _manager.NeighborhoodService.GetNeighborhoods(false);
            return Ok(values);
        }
    }
}
