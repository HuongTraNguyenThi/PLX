using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class GetCustomerResponse : ResultMessageResponse
    {
        public CustomerUpdates Customer { get; set; }
        public ICollection<VehicleResponse> Vehicles { get; set; }
        public ICollection<LinkedCardResponse> LinkedCards { get; set; }
        public ICollection<QuestionResponse> QuestionOne { get; set; }
        public ICollection<QuestionResponse> QuestionTwo { get; set; }
    }
}
