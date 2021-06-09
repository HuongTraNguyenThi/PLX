using PLX.API.Data.Models;

namespace PLX.API.Data.DTO
{
    public class BaseResponse<T> where T : BaseEntity
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Resource { get; private set; }
        public BaseResponse(T resource)
        {
            Success = false;
            Message = string.Empty;
            Resource = resource;
        }
        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }
    }
}