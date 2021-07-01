using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    public class CustomerLog : BaseEntity
    {
        public int CustomerId { get; set; }
        public DateTime Time { get; set; }
    }
}