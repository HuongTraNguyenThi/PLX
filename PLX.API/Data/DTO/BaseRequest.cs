using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO
{
    public class BaseRequest
    {
        public string RequestId { get; set; }

        public string RequestTime { get; set; }

        public string DeviceName { get; set; }

        public string Channel { get; set; }
    }

}