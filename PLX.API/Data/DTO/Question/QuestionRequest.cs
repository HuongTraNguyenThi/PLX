using System;

namespace PLX.API.Data.DTO
{
    public class QuestionRequest
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int RecordType { get; set; }
    }
}