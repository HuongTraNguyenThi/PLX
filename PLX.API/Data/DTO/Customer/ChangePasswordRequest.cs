

namespace PLX.API.Data.DTO.Customer
{
    public class ChangePasswordRequest : BaseRequest
    {
        public string Phone { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public int Type { get; set; }
        public string OtpCode { get; set; }
        public int QuestionId1 { get; set; }
        public string Answer1 { get; set; }
        public int QuestionId2 { get; set; }

        public string Answer2 { get; set; }
    }
}