using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}