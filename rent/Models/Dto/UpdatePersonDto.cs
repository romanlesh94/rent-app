using System.ComponentModel.DataAnnotations;

namespace PersonApi.Entities
{
    public class UpdatePersonDto : BaseEntity
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}
