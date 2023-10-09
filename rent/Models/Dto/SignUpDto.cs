using System.ComponentModel.DataAnnotations;

namespace PersonApi.Models.Dto
{
    public class SignUpDto
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
    }
}
