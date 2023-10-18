using Microsoft.AspNetCore.Mvc;
using PersonApi.Entities;
using PersonApi.Entities.Exceptions;
using PersonApi.Models.Dto;
using PersonApi.Services;
using System;
using System.Threading.Tasks;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync(LoginDto loginDto)
        {
            var result = await _service.LogInAsync(loginDto);

            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(SignUpDto signUpDto)
        {
            var result = await _service.SignUpAsync(signUpDto);

            return Ok(result);
        }

        [HttpPost("verifyPhoneNumber")]
        public async Task<IActionResult> VerifyPhoneNumberAsync(CheckSmsCodeDto checkSmsCodeDto)
        {
            var result = await _service.VerifyPhoneNumber(checkSmsCodeDto);
            return Ok(result);
        }

        [HttpGet("getPerson/{id}")]
        public async Task<IActionResult> GetPersonByIdAsync(long id)
        {
            var result = await _service.GetPersonByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("updatePerson")]
        public async Task<IActionResult> UpdatePersonAsync(UpdatePersonDto person)
        {
            await _service.UpdatePersonAsync(person);

            return Ok("The user has been updated");
        }
    }
}
