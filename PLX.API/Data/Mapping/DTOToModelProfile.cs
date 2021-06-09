
using AutoMapper;
using PLX.API.Data.Models;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using System.Collections.Generic;

namespace Supermarket.API.Data.Mapping
{
    public class DTOToModelProfile : Profile
    {
        public DTOToModelProfile()
        {
            CreateMap<CustomerRegister, Customer>()
            .ForMember(customer => customer.Name, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerBasic.Name))
            .ForMember(customer => customer.Phone, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerBasic.Phone))
            .ForMember(customer => customer.Password, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerBasic.Password))
            .ForMember(customer => customer.Email, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerBasic.Email))
            .ForMember(customer => customer.CardID, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.CardId))
            .ForMember(customer => customer.Date, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.Date))
            .ForMember(customer => customer.Gender, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.Gender))
            .ForMember(customer => customer.TaxCode, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.TaxCode))
            .ForMember(customer => customer.ProvinceId, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.ProvinceId))
            .ForMember(customer => customer.DistrictId, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.DistrictId))
            .ForMember(customer => customer.WardId, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.WardId))
            .ForMember(customer => customer.Address, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerCard.Address))
            .ForMember(customer => customer.CustomerTypeId, opt => opt.MapFrom(custReg => custReg.CustomerInfo.CustomerBasic.CustomerTypeId));

            CreateMap<QuestionDTO, CustomerQuestion>();

            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<LinkedCardDTO, LinkedCard>();

        }
    }
}