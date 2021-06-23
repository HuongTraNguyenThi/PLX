namespace PLX.API.Data.DTO
{
    public class ApiErrorResponse : APIResponse
    {
        public ApiErrorResponse(string resultCode, object[] arguments = null)
        {
            Result = new ApiResponseResult
            {
                Success = false,
                ResultCode = resultCode,
                Arguments = arguments
            };
            Data = new ErrorMessageResponse(resultCode);
        }
    }
}