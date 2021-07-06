namespace PLX.API.Data.DTO.Authentication
{
    public class OTPResponse : ResultMessageResponse
    {
        public string Message { get; set; }
        public OTPResponse(string message)
        {
            this.Message = message;
        }
    }
}