namespace PLX.API.Data.DTO.Customer
{
    public class CustomerCard
    {
        public string CardId { get; set; }
        public string Date { get; set; }
        public string Gender { get; set; }
        public string TaxCode { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string Address { get; set; }
    }
}
