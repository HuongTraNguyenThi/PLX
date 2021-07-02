using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PLX.API.Data.DTO.LinkedCard;
using PLX.API.Data.DTO.Vehicle;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerUpdateRequest : BaseRequest
    {
        public CustomerUpdates Customer { get; set; }
        public List<QuestionRequest> Questions { get; set; }
        public List<VehicleRequest> Vehicles { get; set; }
        public List<LinkedCardRequest> LinkedCards { get; set; }
    }
}
