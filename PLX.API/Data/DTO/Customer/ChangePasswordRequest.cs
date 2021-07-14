

namespace PLX.API.Data.DTO.Customer
{
    public class ChangePasswordRequest : BaseRequest
    {

        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

    }
}