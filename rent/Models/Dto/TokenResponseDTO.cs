using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonService.Entities.Dto
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }
}
