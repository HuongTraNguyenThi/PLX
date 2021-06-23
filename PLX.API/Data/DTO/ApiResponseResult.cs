namespace PLX.API.Data.DTO
{
    public class ApiResponseResult
    {
        public bool Success { get; set; }
        public string ResultCode { get; set; }
        public object[] Arguments { get; set; }

    }
}