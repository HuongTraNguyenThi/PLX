using System;
using System.Collections.Generic;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Data.DTO.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; }
        public CustomerResponse Customer { get; set; }
        public ICollection<QuestionDTO> Questions { get; set; }
        public List<VehicleDTO> Vehicles { get; set; }
        public List<LinkedCardDTO> LinkedCards { get; set; }
    }
}