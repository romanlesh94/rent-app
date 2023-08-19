using rent.Entities;
using rent.Entities.Dto;
using rent.Models;
using rent.Models.Dto;
using System.Threading.Tasks;

namespace rent.Services
{
    public interface IAccountService
    {
        Task<AuthToken> LogInAsync(LoginDto loginDto);
        Task<AuthToken> SignUpAsync(SignUpDto signUpDto);
    }
}
