using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerBasic
    {
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Phone { get; set; }

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public List<QuestionDTO> Questions { get; set; }
        [Required]
        public int CustomerTypeId { get; set; }

    }
}