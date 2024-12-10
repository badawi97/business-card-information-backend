using AutoMapper;
using BusinessCardInformation.Application.Contracts.Cards.Dto;
using BusinessCardInformation.Domain.Cards.Entities;

namespace BusinessCardInformation.Application
{
    public class BusinessCardInformationApplicationAutoMapperProfile : Profile
    {
        public BusinessCardInformationApplicationAutoMapperProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<Card, CreateCardDto>().ReverseMap();
            CreateMap<Card, UpdateCardDto>().ReverseMap();

        }
    }
}
