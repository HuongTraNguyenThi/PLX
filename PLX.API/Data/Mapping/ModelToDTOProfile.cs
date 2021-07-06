using System;
using System.Collections.Generic;
using AutoMapper;
using PLX.API.Data.Models;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using PLX.API.Helpers;
using PLX.API.Data.DTO.Authentication;
using PLX.API.Data.DTO.Vehicle;
using PLX.API.Data.DTO.LinkedCard;

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

            CreateMap<Vehicle, VehicleResponse>();
            CreateMap<LinkedCard, LinkedCardResponse>();
            CreateMap<CustomerQuestion, QuestionResponse>()
            .ForMember(question => question.Id, opt => opt.MapFrom(customer => customer.QuestionId));

            CreateMap<Vehicle, VehicleRequest>();
            CreateMap<LinkedCard, LinkedCardRequest>();
            CreateMap<CustomerQuestion, QuestionRequest>()
            .ForMember(question => question.Id, opt => opt.MapFrom(customer => customer.QuestionId));

            CreateMap<Customer, CustomerResponse>();

            CreateMap<Customer, CustomerUpdateResponse>()
                .ForMember(auth => auth.Customer, opt => opt.MapFrom(customer => customer));

            CreateMap<Customer, AuthenticationResponse>()
                .ForMember(auth => auth.Customer, opt => opt.MapFrom(customer => customer));

            CreateMap<Customer, CustomerUpdates>();

            CreateMap<Customer, GetCustomerResponse>()
             .ForMember(auth => auth.Customer, opt => opt.MapFrom(customer => customer));

        }
    }
}
