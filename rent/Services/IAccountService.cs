using Microsoft.AspNetCore.Http;
using PersonApi.Entities;
using PersonApi.Models;
using PersonApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface IAccountService
    {
        Task<TokensDto> LogInAsync(LoginDto loginDto);
        Task<long> SignUpAsync(SignUpDto signUpDto);
        Task<GetPersonDto> GetPersonByIdAsync(long id);
        Task UpdatePersonAsync(UpdatePersonDto person);
        Task<TokensDto> VerifyPhoneNumber(CheckSmsCodeDto checkSmsCodeDto);
        Task AddPersonImageAsync(long personId, IFormFile file);
        Task<TokensDto> RefreshTokenVerificationAsync(string refreshToken);
        Task SendRoleChangeRequest(long personId);
        Task<List<RoleChangeRequest>> GetAllPendingRequests();
        Task ApproveRoleChangeRequest(long personId);
    }
}
