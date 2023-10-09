using PersonApi.Entities;
using PersonApi.Models;
using PersonApi.Models.Dto;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface IAccountService
    {
        Task<AuthToken> LogInAsync(LoginDto loginDto);
        Task<AuthToken> SignUpAsync(SignUpDto signUpDto);
        Task<Person> GetPersonByIdAsync(long id);
    }
}
