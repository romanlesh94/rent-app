using PersonApi.Models.Enums;

namespace PersonApi.Models.Dto
{
    public class TokensDto
    {
        public long PersonId { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public Roles Role { get; set; }
    }
}
