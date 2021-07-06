using System;
namespace PLX.Persistence.Model
{
    public class LogAPI : BaseEntity
    {
        public string RequestId { get; set; }
        public string ContentRequest { get; set; }
        public string ApiName { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string Device { get; set; }
        public string Channel { get; set; }
    }
}