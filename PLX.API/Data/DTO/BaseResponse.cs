using System.ComponentModel.DataAnnotations;
using PLX.API.Data.Models;

namespace PLX.API.Data.DTO
{
    public class BaseResponse
    {
        public string ResponseId { get; set; }
        public string ResponseTime { get; set; }
    }
}