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
    public class DonationMovementManager : IDonationMovementService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        
        public DonationMovementManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _manager = repositoryManager;
            _mapper = mapper;
        }
        public async Task CreteDonationMovement()
        {
            List<CreateDonationMovementDto> list = new List<CreateDonationMovementDto>();
            var entity = await _manager.PersonalNotification.GetAllPersonalNotifications(false);
           
            var donationMovementDictionary = (await GetDonationMovementsAsync(false)).OrderByDescending(p => p.DonationDate)
              .ToDictionary(m => m.DonationId, m => m.PersonId);
            foreach (var item in entity)
            {

                var notId = item.NotificationId;
                var notification = await _manager.Notification.GetNotificationsById(notId, false);
                var donId = notification.DonationId;


                if (!donationMovementDictionary.ContainsKey(notification.DonationId))
                {
                    var donation = await _manager.Donation.FindByCondition(x => x.DonationId.Equals(donId), false).SingleOrDefaultAsync();
                    var donTypeId = donation.DonationTypeId;

                    CreateDMDto newEntity = new()
                    {
                        DonationId = donId,
                        PersonId = item.PersonalInformationId,
                        DonationDate = DateTime.Now,
                        DonationTypeId = donTypeId,
                        DonationAmount = 1,
                        Description = "xyz",
                        MovementStatusId = 1

                    };
                    var varlik = _mapper.Map<DonationMovement>(newEntity);
                    _manager.DonationMovement.CreteDonationMovement(varlik);
                    await _manager.SaveAsync();


                }
            }
        }
        public async Task UpdateDonationMovementStatus(int id, int statusId, bool trackChanges)
        {
            if (id == null || statusId == null)
                throw new Exception("Id veya StatusId boş");
            _manager.DonationMovement.UpdateDonationMovementStatus(id,statusId,trackChanges);
            await _manager.SaveAsync();
        }
        public async Task<List<DonationMovement>> GetDonationMovementsAsync(bool trackChanges)
        {
            return await _manager.DonationMovement.GetDonationMovementsAsync(trackChanges);
        }
        public async Task<List<PreviousDonationsDto>> GetDonationMovementsByPersonId(int id, bool trackChanges)
        {
            if (id == null)
                throw new Exception("ID BOŞ OLAMAZ...");
            
            List<PreviousDonationsDto> mappedEntity = new List<PreviousDonationsDto>();
            
            var entity = await _manager.DonationMovement.GetDonationMovementsByPersonId(id, trackChanges);

            List<Donation> donations = await _manager.Donation.GetAllDonation(trackChanges);
            
            foreach (var donationMovement in entity)
            {
                var type = _manager.DonationMovement.GetDonationTypeNameById(donationMovement.DonationTypeId);

                if (donationMovement.PersonId == id) 
                {
                    foreach (var donation in donations)
                    {
                        var recepient = _manager.Recepient.FindByCondition(x => x.Id.Equals(donation.RecepientId), trackChanges).FirstOrDefault();
                        var hospital = _manager.DonationMovement.GetHospitalByID(donation.HospitalId,trackChanges);
                        if (donationMovement.DonationId == donation.DonationId)
                        {
                            mappedEntity.Add(new PreviousDonationsDto
                            {
                                DateandTime = donationMovement.DonationDate,
                                DonationType = type.DonationTypeName,
                                Hospital = hospital.HospitalName,
                                Recepient = recepient.Name[0] + "." + recepient.Surname[0]

                            });
                        }


                    }
                }
            }
        
            return mappedEntity;
        }
        public async Task<DonationMovement> GetOneDonationMovementById(int id, bool trackChanges)
        {
            if (id == null)
                throw new Exception("ID BOŞ OLAMAZ...");
            return await _manager.DonationMovement.GetOneDonationMovementById(id, trackChanges);

        }
        public List<GetHospitalDto> GetHospitalList()
        {
            var list = _manager.DonationMovement.GetHospitalLists();
            List<GetHospitalDto> hospitals = new List<GetHospitalDto>();
            foreach (var item in list)
            {
                hospitals.Add(new GetHospitalDto()
                {
                    ID = item.ID,
                    HospitalName = item.HospitalName
                });
            }
            return hospitals;
        }
        

        



      
    }
}
