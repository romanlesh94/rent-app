using System.ComponentModel.DataAnnotations;

namespace HouseApi.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
