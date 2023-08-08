using rent.Entities;
using rent.Entities.DTO;
using System.Threading.Tasks;

namespace rent.Services
{
    public interface IAccountService
    {
        Task<ResponseDTO> LogInAsync(string login, string password);
        Task<Person> SignUpAsync(string login, string password, string email, string country);
    }
}
