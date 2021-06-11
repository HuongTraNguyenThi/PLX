using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    public class LogAPI : BaseEntity
    {

        public int RequestId { get; set; }
        public string ContentRequest { get; set; }
        public string ApiName { get; set; }

        public DateTime RequestTime { get; set; }
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }
}