using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerBasic
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<QuestionDTO> Questions { get; set; }
        public int CustomerTypeId { get; set; }

    }
}