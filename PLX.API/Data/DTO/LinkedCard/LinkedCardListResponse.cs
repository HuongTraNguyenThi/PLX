using System;
using System.Collections.Generic;

namespace PLX.API.Data.DTO
{
    public class LinkedCardListResponse : ResultMessageResponse
    {
        public List<LinkedCardResponse> LinkedCards { get; set; }
    }
}