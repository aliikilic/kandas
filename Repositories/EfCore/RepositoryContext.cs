using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Emit;

namespace Repositories.EfCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<District> Districts{ get; set; }
        public DbSet<Donation> Donations{ get; set; }
        public DbSet<DonationMovement> DonationMovements{ get; set; }
        public DbSet<DonationType> DonationTypes{ get; set; }
        public DbSet<Hospital> Hospitals{ get; set; }
        public DbSet<Neighborhood> Neighborhoods{ get; set; }
        public DbSet<Notification> Notifications{ get; set; }
        public DbSet<PersonalInformation> PersonalInformations{ get; set; }
        public DbSet<PersonInquiryForm> PersonInquiryForms{ get; set; }
        public DbSet<Recepient> Recepients{ get; set; }
        public DbSet<EducationStatus> EducationStatus { get; set; }
        public DbSet<PersonalInformationNotifications> PersonalInformationNotifications { get; set; }

        



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ModelDefinitionExtensions.ConfigureUserandPersonalInformationRel(builder);
            ModelDefinitionExtensions.ConfigurePersonalInformationandAddressRel(builder);
            ModelDefinitionExtensions.ConfigurePersonalInformationandEducationStatusRel(builder);
            ModelDefinitionExtensions.ConfigureCityDistrictNeighborhoodRel(builder);
            ModelDefinitionExtensions.ConfigureHospitalAdressRel(builder);
            ModelDefinitionExtensions.ConfigureDonationRel(builder);
            ModelDefinitionExtensions.ConfigureDonationMovementRel(builder);
            ModelDefinitionExtensions.ConfigureNotificationRel(builder);
            ModelDefinitionExtensions.ConfigureFormRel(builder);


        }

    }
}
