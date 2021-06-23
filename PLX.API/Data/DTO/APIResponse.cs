namespace PLX.API.Data.DTO
{
    public abstract class APIResponse
    {

        public ApiResponseResult Result { get; set; }
        public object Data { get; set; }
    }
}