
namespace PLX.API.Data.DTO
{
    public class ErrorMessageResponse : BaseResponse
    {
        public ErrorMessageResponse(int code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}