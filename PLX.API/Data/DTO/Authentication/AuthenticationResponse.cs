using System;
using System.Collections.Generic;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Data.DTO.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; }
        public CustomerResponse Customer { get; set; }
        public ICollection<QuestionResponse> Questions { get; set; }
        public List<VehicleResponse> Vehicles { get; set; }
        public List<LinkedCardResponse> LinkedCards { get; set; }
    }
}