using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Mappings
{
    internal class CarWorkshopMappingProfile : Profile //w profilu mapowania definiujemy zasady mapowania
    {
        public CarWorkshopMappingProfile()
        {
            CreateMap<CarWorkshopDto, Domain.Entities.CarWorkshop>() // z czego mapujemy i na co
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails() //definiujemy jak mapowanie ma obsluzyc contactDetails
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDto>()
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street)) //opt to dod. opcja, wybieramy tu z czego mapujemy
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber));
        }
    }
}
