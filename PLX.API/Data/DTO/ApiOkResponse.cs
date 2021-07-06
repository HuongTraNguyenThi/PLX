namespace PLX.API.Data.DTO
{
    public class ApiOkResponse : APIResponse
    {
        public ApiOkResponse(object data, string resultCode, object[] arguments = null)
        {
            Result = new ApiResponseResult
            {
                Success = true,
                ResultCode = resultCode,
                Arguments = arguments,
                DataType = data.GetType().FullName
            };
            Data = data;
        }
    }
}