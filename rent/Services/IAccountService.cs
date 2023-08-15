using rent.Entities;
using rent.Entities.Dto;
using rent.Models.Dto;
using System.Threading.Tasks;

namespace rent.Services
{
    public interface IAccountService
    {
        Task<TokenResponseDto> LogInAsync(LoginDto loginDto);
        Task<TokenResponseDto> SignUpAsync(string login, string password, string email, string country);
    }
}
