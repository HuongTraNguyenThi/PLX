using System;

namespace PLX.API.Data.DTO.LinkedCard
{
    public class LinkedCardRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public int RecordType { get; set; }
    }
}