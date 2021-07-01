using System;
using System.Collections.Generic;
using AutoMapper;
using PLX.API.Data.Models;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using PLX.API.Helpers;

namespace PLX.API.Data.Mapping
{
    public class ModelToDTOProfile : Profile
    {
        public ModelToDTOProfile()
        {
            CreateMap<Customer, CustomerRegisterResponse>()
                .ForMember(customerDto => customerDto.IdCustomer, opt => opt.MapFrom(customer => customer.Id));
            CreateMap<Customer, CustomerDTO>()
            .ForMember(customerDto => customerDto.Date, opt => opt.MapFrom(customer => DateTimeConvert.DateToString(customer.Date)));
            CreateMap<Question, ListItem>()
            .ForMember(listItem => listItem.Value, opt => opt.MapFrom(question => question.Id))
            .ForMember(listItem => listItem.Display, opt => opt.MapFrom(question => question.Content));

            CreateMap<Province, ListItem>()
            .ForMember(listItem => listItem.Value, opt => opt.MapFrom(province => province.Id))
            .ForMember(listItem => listItem.Display, opt => opt.MapFrom(province => province.Name));
            CreateMap<District, ListItem>()
            .ForMember(listItem => listItem.Value, opt => opt.MapFrom(district => district.Id))
            .ForMember(listItem => listItem.Display, opt => opt.MapFrom(district => district.Name));
            CreateMap<Ward, ListItem>()
            .ForMember(listItem => listItem.Value, opt => opt.MapFrom(ward => ward.Id))
            .ForMember(listItem => listItem.Display, opt => opt.MapFrom(ward => ward.Name));
            CreateMap<VehicleType, ListItem>()
            .ForMember(listitem => listitem.Value, opt => opt.MapFrom(vehicleType => vehicleType.Id))
            .ForMember(listitem => listitem.Display, opt => opt.MapFrom(vehicleType => vehicleType.Name));

        }
    }
}
