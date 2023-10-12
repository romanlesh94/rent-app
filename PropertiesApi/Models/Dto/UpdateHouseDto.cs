using System.ComponentModel.DataAnnotations;

namespace HouseApi.Models.Dto
{
    public class UpdateHouseDto: BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(300)]
        public string Rules { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
