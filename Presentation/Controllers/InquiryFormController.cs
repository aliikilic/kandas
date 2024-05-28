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
    [Route("api/inquiry")]
    public class InquiryFormController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public InquiryFormController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost("InquiryForm")]
        public async Task<IActionResult> CreateNewForm(CreateInquiryFormDto form, int id)
        {
            await _manager.InquiryFormService.CreateInquiryForm(form, id);
            return NoContent();
        }
        [HttpPut("Update Form")]
        public IActionResult UpdateForm(UpdateInquiryDto form, int id)
        {
            _manager.InquiryFormService.UpdateInquiryForm(form, id);
            return NoContent();
        }
    }
}
