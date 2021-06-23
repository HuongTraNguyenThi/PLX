namespace PLX.API.Data.DTO
{
    public class ApiOkResponse : APIResponse
    {
        public ApiOkResponse(object data, string resultCode = "11003", object[] arguments = null)
        {
            Result = new ApiResponseResult
            {
                Success = true,
                ResultCode = resultCode,
                Arguments = arguments
            };
            Data = data;
        }
    }
}