using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class OTPValidateRequest : BaseRequest
    {


        public string Phone { get; set; }

        public string OtpCode { get; set; }

    }
}
