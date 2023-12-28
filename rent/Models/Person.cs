using PersonApi.Models;
using PersonApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.Entities
{
    public class Person : BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        [EmailAddress]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Country { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        public string PhoneNumber { get; set; }

        public bool IsPhoneVerified { get; set; }

        [Required]
        public Roles Role { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
