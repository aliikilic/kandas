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
    [Route("api/notification")]
    public class NotificationsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public NotificationsController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("GetAllPersonsNotifications")]
        public IActionResult GetAllNotifications() 
        {
            var result = _manager.NotificationService.GetPersonalInformationNotifications();
            return Ok(result);
        }
        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateNewNotification(CreateNotificationDto dto)
        {
            await _manager.NotificationService.CreateNotification(dto);
            return NoContent();
        }
        [HttpPut("UpdateNotificationStatus")]
        public async Task<IActionResult> UpdateNotificationStatus(int notificationId)
        {
            await _manager.NotificationService.UpdateNotificationStatus(notificationId,true);
            return NoContent();
        }
        [HttpGet("GetPersonalNotificationList")]
        public async Task<IActionResult> GetPersonalNotificationList(int id)
        {
            var result = await _manager.NotificationService.NotificationsByPersonId(id);
            return Ok(result);
        }
    }
}
