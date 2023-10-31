using PersonApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.Models.Dto
{
    public class GetPersonDto : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPhoneVerified { get; set; }
        public string ImageUrl { get; set; }
    }
}
