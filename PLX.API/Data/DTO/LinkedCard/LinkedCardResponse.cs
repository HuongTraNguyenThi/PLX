using System;
using PLX.API.Constants;

namespace PLX.API.Data.DTO
{
    public class LinkedCardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public int RecordType { get; set; } = RecordTypes.ExistRecord;
    }
}