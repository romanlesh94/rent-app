using PersonApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.Models
{
    public class PersonImage : BaseEntity
    {
        [Required]
        public string ImageUrl { get; set; }
        public long PersonId { get; set; }
        [Required]
        public Person Person { get; set; }
    }
}
