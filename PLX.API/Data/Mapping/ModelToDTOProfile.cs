using System;
using System.Collections.Generic;
using AutoMapper;
using PLX.API.Data.Models;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Data.Mapping {
    public class ModelToDTOProfile : Profile {
        public ModelToDTOProfile() {
            CreateMap<Customer, CustomerResponse>();

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
        }
    }
}
