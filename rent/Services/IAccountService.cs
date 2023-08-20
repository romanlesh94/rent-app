using PersonService.Entities;
using PersonService.Entities.Dto;
using PersonService.Models;
using PersonService.Models.Dto;
using System.Threading.Tasks;

namespace PersonService.Services
{
    public interface IAccountService
    {
        Task<AuthToken> LogInAsync(LoginDto loginDto);
        Task<AuthToken> SignUpAsync(SignUpDto signUpDto);
    }
}
