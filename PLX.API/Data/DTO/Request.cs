using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO
{
    public class Request
    {
        //[Required]
        public string RequestId { get; set; }

        //[Required]
        public string RequestTime { get; set; }
    }
}