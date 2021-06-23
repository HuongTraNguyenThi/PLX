namespace PLX.API.Data.DTO.Customer
{
    public class OTPDTO : BaseResponse
    {
        public string Phone { get; set; }
        public bool Active { get; set; }
        public int OtpCode { get; set; }

    }
}
