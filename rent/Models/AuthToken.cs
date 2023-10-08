using PersonApi.Entities;

namespace PersonApi.Models
{
    public class AuthToken: BaseEntity
    {
        public string Token { get; set; }
    }
}
