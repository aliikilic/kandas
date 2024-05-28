using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EfCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationManager : INotificationService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly RepositoryContext _context;

        public NotificationManager(IRepositoryManager manager, IMapper mapper, RepositoryContext context)
        {
            _manager = manager;
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateNotification(CreateNotificationDto notification)
        {
            if (notification == null)
                throw new Exception("BOŞ OLAMAZ...");
            var entity = _mapper.Map<Notification>(notification);
            _manager.Notification.CreateNotification(entity);
            await _manager.SaveAsync();

        }

        public List<Dictionary<string, object>> GetPersonalInformationNotifications()
        {
             var list = _context.Set<Dictionary<string, object>>("PersonalInformationNotification")
            .FromSqlRaw("SELECT * FROM PersonalInformationNotification")
            .ToList();
            return list;
            
        }

        public async Task<List<NotificationListDto>> NotificationsByPersonId(int id)
        {
            var personsnotification = await _manager.PersonalNotification.GetPersonalNotificationsByPersonId(id);

            List<int> donationIds = new List<int>();
            List<NotificationListDto> notificationList = new List<NotificationListDto>();
            foreach (var noti in personsnotification) 
            {
                var notification = await _manager.Notification.GetNotificationsById(noti.NotificationId,false);
                donationIds.Add(notification.DonationId);
                var donation = await _manager.Donation.GetDonationById(notification.DonationId, false);
                var hospital = _manager.DonationMovement.GetHospitalByID(donation.HospitalId,false);
                var donationType = _manager.DonationMovement.GetDonationTypeNameById(donation.DonationTypeId);
                var recepient = await _manager.Recepient.GetRecepientsById(donation.RecepientId,false);

                notificationList.Add(new NotificationListDto()
                {
                    NotificationId = notification.NotificationId,
                    Hospital = hospital.HospitalName,
                    HospitalAddress = hospital.AddressDescription,
                    Name= recepient.Name,
                    Surname = recepient.Surname,
                    BloodType = recepient.BloodType,
                    DonationType = donationType.DonationTypeName,
                    NecessityAmount =donation.NecessityAmount,
                    NotificationTime = noti.InsertDate.ToShortTimeString()
                });
                
            }

            return notificationList;
            #region beklesin
            //var donId = await _manager.Notification.FindByCondition(n => n.NotificationId.Equals(personsnotification), false).ToListAsync();
            //List<int> recepIds = new List<int>();
            //foreach (var item in donId)
            //{
            //    var recepients = await _manager.Donation.GetRecepientIdByDonId(item.DonationId,false);

            //    foreach (var recepId in recepients)
            //    {

            //        recepIds.Add(recepId);

            //    }

            //}

            //List<RecepientNSBDto> recepientNSBDtos = new List<RecepientNSBDto>();

            //foreach (var item in recepIds)
            //{
            //    recepientNSBDtos.Add(await _manager.Recepient.GetRecepientsById(item, false));
            //}
            #endregion
        }

        public async Task UpdateNotificationStatus(int notificationId, bool trackChanges)
        {
            await _manager.Notification.UpdateNotificationStatus(notificationId,trackChanges);
            await _manager.SaveAsync();
        }

        
    }
}
