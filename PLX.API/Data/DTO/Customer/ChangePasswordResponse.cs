

namespace PLX.API.Data.DTO.Customer
{
    public class ChangePasswordResponse : ResultMessageResponse
    {
        public string Message { get; set; }
        public ChangePasswordResponse(string message)
        {
            this.Message = message;
        }
    }
}