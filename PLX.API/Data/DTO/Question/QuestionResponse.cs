using System;
using PLX.API.Constants;

namespace PLX.API.Data.DTO
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int RecordType { get; set; } = RecordTypes.ExistRecord;
    }
}