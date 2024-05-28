using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public MailController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> sendkey(MailModel model)
        {
            model = await _serviceManager.MailService.SendPasswordRecoveryCode(model);

            var x = model.Body.Substring(0,5);

            var zaman = model.SendTime;

            TimeSpan time = zaman.TimeOfDay;

            TimeSpan yeni = time.Add(TimeSpan.FromSeconds(160));

            var result = new {key= x, createTime=time,gecerlilik=yeni};
            return Ok(result);
        }
    }
}
