
namespace PLX.API.Data.DTO
{
    public class ErrorMessageResponse : BaseResponse
    {
        public ErrorMessageResponse(string code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}