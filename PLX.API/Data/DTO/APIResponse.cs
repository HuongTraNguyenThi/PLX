namespace PLX.API.Data.DTO
{
    public class APIResponse
    {
        public bool Success { get; private set; }
        public ErrorMessageResponse ErrorMessage { get; private set; }
        public BaseResponse Resource { get; private set; }
        public OTPResponse OtpResponse { get; set; }

        public APIResponse(BaseResponse resource)
        {
            Success = true;
            ErrorMessage = null;
            Resource = resource;
        }
        public APIResponse(int code, string message)
        {
            Success = false;
            ErrorMessage = new ErrorMessageResponse(code, message);
            Resource = default;
        }
    }
}