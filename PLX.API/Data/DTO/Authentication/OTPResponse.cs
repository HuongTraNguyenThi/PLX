namespace PLX.API.Data.DTO.Authentication
{
    public class OTPResponse : BaseResponse
    {
        public string Message { get; set; }
        public OTPResponse(string message)
        {
            this.Message = message;
        }


    }
}