using System.ComponentModel.DataAnnotations;

namespace HouseApi.Models
{
    public class Image : BaseEntity
    {
        [Required]
        public string ImageUrl { get; set; }
        public long HouseId { get; set; }
        [Required]
        public House House { get; set; }
    }
}
