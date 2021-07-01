using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Authentication
{
    public class OTPValidateRequest : BaseRequest
    {
        public string Phone { get; set; }

        public string OtpCode { get; set; }

    }
}
