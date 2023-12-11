using PersonApi.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.Models
{
    public class RefreshToken : BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Token { get; set; }
        public long PersonId { get; set; }
        
        [Required]
        public DateTime CreatedDtm { get; set; }
        
        [Required]
        public Person Person { get; set; }
    }
}
