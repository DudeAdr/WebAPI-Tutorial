﻿using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshopService;
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
        public CarWorkshopMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();
            CreateMap<CarWorkshopDto, Domain.Entities.CarWorkshop>() // z czego mapujemy i na co
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails() //definiujemy jak mapowanie ma obsluzyc contactDetails
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id || user.isInRole("Moderator"))))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street)) //opt to dod. opcja, wybieramy tu z czego mapujemy
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber));

            CreateMap<CarWorkshopDto, EditCarWorkshopCommand>();

            CreateMap<CarWorkshopServiceDto, Domain.Entities.CarWorkshopService>()
                .ReverseMap(); //.ReverseMap() -- automatycznie zarejestruje mapowanie odwrócone
        }
    }
}
