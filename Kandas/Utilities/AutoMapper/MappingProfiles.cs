using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Kandas.Utilities.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRegisterationDto,User>().ReverseMap();
            CreateMap<UserForAuthenticationDto,User>().ReverseMap();
            CreateMap<PersonDto,PersonalInformation>().ReverseMap();
            CreateMap<UpdateInquiryDto,PersonInquiryForm>().ReverseMap();
            CreateMap<CreateInquiryFormDto, PersonInquiryForm>().ReverseMap();
            CreateMap<CreateRecepientDto,Recepient>().ReverseMap();
            CreateMap<CreateDonationDto,Donation>().ReverseMap();
            CreateMap<CreateDonationMovementDto,DonationMovement>().ReverseMap();
            CreateMap<CreateDonationMovementDto, CreateDMDto>().ReverseMap();
            CreateMap<DonationMovement, CreateDMDto>().ReverseMap();
            CreateMap<CreateNotificationDto,Notification>().ReverseMap();
            CreateMap<PreviousDonationsDto,DonationMovement>().ReverseMap();
            CreateMap<GetHospitalDto,Hospital>().ReverseMap();
            CreateMap<GetDonationTypeDto,DonationType>().ReverseMap();
            CreateMap<GetCitiesDto,City>().ReverseMap();
            CreateMap<GetDistrictsDto,District>().ReverseMap();
            CreateMap<GetNeighborhoodsDto,Neighborhood>().ReverseMap();

            
        }
    }
}
