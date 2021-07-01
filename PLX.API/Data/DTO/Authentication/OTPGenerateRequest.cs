namespace PLX.API.Data.DTO.Authentication
{
    public class OTPGenerateRequest : BaseRequest
    {
        public string Phone { get; set; }
        public string TransactionType { get; set; }
    }
}