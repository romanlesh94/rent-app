using PersonApi.Entities;
using PersonApi.Models;
using PersonApi.Models.Dto;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface IAccountService
    {
        Task<AuthToken> LogInAsync(LoginDto loginDto);
        Task<long> SignUpAsync(SignUpDto signUpDto);
        Task<Person> GetPersonByIdAsync(long id);
        Task UpdatePersonAsync(UpdatePersonDto person);
        Task<AuthToken> VerifyPhoneNumber(CheckSmsCodeDto checkSmsCodeDto);
    }
}
