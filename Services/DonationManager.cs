using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using MimeKit.Encodings;
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
    public class DonationManager : IDonationService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly RepositoryContext _context;
        private readonly NotificationManager _notificationManager;
        private readonly DonationMovementManager _donationMovementManager;
        private int donID;
        public DonationManager(IRepositoryManager manager, IMapper mapper, RepositoryContext context, NotificationManager notificationManager, DonationMovementManager donationMovementManager)
        {
            _manager = manager;
            _mapper = mapper;
            _context = context;
            _notificationManager = notificationManager;
            _donationMovementManager = donationMovementManager;
        }

        public async Task CreateDonation(CreateDonationDto entity)
        {
            if (entity is null)
                throw new Exception("Donation bilgisi boş");
            var donation = _mapper.Map<Donation>(entity);
            _manager.Donation.CreateDonation(donation);
            await _manager.SaveAsync();
             donID = donation.DonationId;
            await CreateNotificationWithDonationId(donID);
            await _donationMovementManager.CreteDonationMovement();
            
        }

        public async Task<List<Donation>> GetAllDonation(bool trackChanges)
        {
            return await _manager.Donation.GetAllDonation(trackChanges);
        }

        public Task<Donation> GetDonationByRecepientId(int id, bool trackChanges)
        {
            return _manager.Donation.GetDonationByRecepientId(id,trackChanges);
        }

        private async Task CreateNotificationWithDonationId(int id)
        {
                var notification = new CreateNotificationDto()
                {
                    DonationId = id,
                    NotificationDate = DateTime.Now,
                    IsActive = true
                };
                await _notificationManager.CreateNotification(notification);

        }
    }
}

