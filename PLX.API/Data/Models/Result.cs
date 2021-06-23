using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    public class Result : BaseEntity
    {

        public string Code { get; set; }


        public string Message { get; set; }
    }
}