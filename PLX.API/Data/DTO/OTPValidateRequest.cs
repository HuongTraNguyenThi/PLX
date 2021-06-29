using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class OTPValidateRequest : BaseRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập số điện thoại")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        public string OtpCode { get; set; }

    }
}
