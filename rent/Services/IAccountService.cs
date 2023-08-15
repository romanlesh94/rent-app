using rent.Entities;
using rent.Entities.DTO;
using System.Threading.Tasks;

namespace rent.Services
{
    public interface IAccountService
    {
        Task<TokenResponseDTO> LogInAsync(string login, string password);
        Task<TokenResponseDTO> SignUpAsync(string login, string password, string email, string country);
    }
}
