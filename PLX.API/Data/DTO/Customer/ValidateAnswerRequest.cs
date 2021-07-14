

namespace PLX.API.Data.DTO.Customer
{
    public class ValidateAnswerRequest : BaseRequest
    {
        public string Phone { get; set; }
        public int QuestionId1 { get; set; }
        public string Answer1 { get; set; }
        public int QuestionId2 { get; set; }
        public string Answer2 { get; set; }

    }
}