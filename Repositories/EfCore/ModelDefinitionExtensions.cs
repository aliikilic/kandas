using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class ModelDefinitionExtensions
    {
        public static void ConfigureUserandPersonalInformationRel(ModelBuilder builder)
        {

            

            builder.Entity<PersonalInformation>()
                .HasOne(p => p.User)
                .WithOne(x => x.PersonalDetails)
                .HasForeignKey<PersonalInformation>(u => u.UserId);

        }
        public static void ConfigurePersonalInformationandAddressRel(ModelBuilder builder)
        {
            builder.Entity<PersonalInformation>()
                .HasOne(p => p.ResidanceCity)
                .WithMany(c => c.PersonalDetails)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PersonalInformation>()
                .HasOne(p => p.BirthPlace)
                .WithMany(c => c.BirthPlaceDetails)
                .HasForeignKey(x => x.BirthPlaceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PersonalInformation>()
                .HasOne(p => p.ResidanceDistrict)
                .WithMany(d => d.PersonalDetails)
                .HasForeignKey(p => p.ResidanceDistrictId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PersonalInformation>()
                .HasOne(p => p.ResidanceNeighborhood)
                .WithMany(d => d.PersonalDetails)
                .HasForeignKey(p => p.ResidanceNeighborhoodId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public static void ConfigurePersonalInformationandEducationStatusRel(ModelBuilder builder)
        {
            builder.Entity<PersonalInformation>()
                .HasOne(p => p.PersonEducationStatus)
                .WithMany(e => e.PersonalDetails)
                .HasForeignKey(p => p.PersonEducationStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public static void ConfigureCityDistrictNeighborhoodRel(ModelBuilder builder)
        {
            builder.Entity<City>()
                .HasMany(c => c.Districts)
                .WithOne(d => d.City)
                .HasForeignKey(d => d.CityID);

            builder.Entity<District>()
                .HasMany(d => d.Neighborhoods)
                .WithOne(n => n.District)
                .HasForeignKey(n => n.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Neighborhood>()
                .HasOne(n => n.District)
                .WithMany(d => d.Neighborhoods)
                .HasForeignKey(n => n.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Neighborhood>()
                .HasOne(n => n.City)
                .WithMany(c => c.Neighborhoods)
                .HasForeignKey(n => n.CityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public static void ConfigureHospitalAdressRel(ModelBuilder builder)
        {
            builder.Entity<Hospital>()
                .HasOne(h => h.AddressCity)
                .WithMany(c => c.Hospital)
                .HasForeignKey(h => h.AddressCityID);
            builder.Entity<Hospital>()
                .HasOne(h => h.AddressDistrict)
                .WithMany(c => c.Hospital)
                .HasForeignKey(h => h.AddressDistrictID)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Hospital>()
                .HasOne(h => h.AddressNeighbohood)
                .WithMany(c => c.Hospital)
                .HasForeignKey(h => h.AddressNeighbohoodID)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public static void ConfigureDonationRel(ModelBuilder builder)
        {
            builder.Entity<Donation>()
                .HasOne(d => d.Recepient)
                .WithMany(r => r.Donation)
                .HasForeignKey(d => d.RecepientId);

            builder.Entity<Donation>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.Donations)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Donation>()
                .HasOne(d => d.DonationType)
                .WithMany(dt => dt.Donations)
                .HasForeignKey(d => d.DonationTypeId)
                .OnDelete(DeleteBehavior.NoAction);

           

            builder.Entity<DonationMovement>()
                .HasOne(dm => dm.Donation)
                .WithMany(d => d.DonationMovement)
                .HasForeignKey(dm => dm.DonationId);

            builder.Entity<Recepient>()
                .HasOne(r => r.BirthPlace)
                .WithMany(c => c.BirthPlace)
                .HasForeignKey(r => r.BirthPlaceId);
            builder.Entity<Recepient>().HasKey(r => r.Id);
                
        }
        public static void ConfigureDonationMovementRel(ModelBuilder builder)
        {
            builder.Entity<DonationMovement>()
                .HasOne(dm => dm.PersonalDetails)
                .WithMany(p => p.DonationMovements)
                .HasForeignKey(dm => dm.PersonId);
            builder.Entity<DonationMovement>()
                .HasOne(dm => dm.DonationType)
                .WithMany(dt => dt.DonationMovements)
                .HasForeignKey(dm => dm.DonationTypeId);

            builder.Entity<DonationMovement>()
                .HasOne(dm => dm.MovementStatus)
                .WithMany(ms => ms.DonationMovements)
                .HasForeignKey(dm => dm.MovementStatusId);


         



        }
        public static void ConfigureNotificationRel(ModelBuilder builder)
        {
           
           
            builder.Entity<Notification>()
                .HasOne(n => n.Donation)
                .WithMany(d => d.Notifications)
                .HasForeignKey(n => n.DonationId)
                .OnDelete(DeleteBehavior.NoAction);
          




            builder.Entity<PersonalInformationNotifications>().HasNoKey();
            builder.Entity<PersonalInformationNotifications>().ToTable("PersonalInformationNotification");



      

        }
        public static void ConfigureFormRel(ModelBuilder builder)
        {
            builder.Entity<PersonInquiryForm>()
                .HasOne(p => p.PersonalDetails)
                .WithOne(x => x.PersonInquiryForm)
                .HasForeignKey<PersonInquiryForm>(p => p.PersonalInformationId);
                
        
        }
    }
}
