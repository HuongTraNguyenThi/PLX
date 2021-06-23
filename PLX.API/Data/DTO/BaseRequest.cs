using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO
{
    public class BaseRequest
    {
        [Required]
        [MaxLength(36)]
        public string RequestId { get; set; }

        [Required]
        public string RequestTime { get; set; }

    }

}