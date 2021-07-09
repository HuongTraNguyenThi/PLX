using System;
using PLX.API.Constants;

namespace PLX.API.Data.DTO.LinkedCard
{
    public class LinkedCardRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public int RecordType { get; set; } = RecordTypes.ExistRecord;
    }
}