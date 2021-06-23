using System.ComponentModel.DataAnnotations;
using PLX.API.Data.Models;

namespace PLX.API.Data.DTO
{
    public class BaseResponse
    {
        [Required]
        [MaxLength(36)]
        public string ResponseId { get; set; }
        [Required]
        public string ResponseTime { get; set; }
    }
}