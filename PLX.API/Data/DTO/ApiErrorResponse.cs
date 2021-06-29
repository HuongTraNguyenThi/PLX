namespace PLX.API.Data.DTO
{
    public class ApiErrorResponse : APIResponse
    {
        private string message;

        public ApiErrorResponse(string resultCode, object[] arguments = null)
        {
            Result = new ApiResponseResult
            {
                Success = false,
                ResultCode = resultCode,
                Arguments = arguments
            };
            Data = new ErrorMessageResponse(resultCode, message);
        }
    }
}