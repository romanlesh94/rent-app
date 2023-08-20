using Microsoft.AspNetCore.Mvc;
using PersonService.Entities.Exceptions;
using PersonService.Models.Dto;
using PersonService.Services;
using System;
using System.Threading.Tasks;

namespace PersonService.Controllers
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

        [HttpPost("/login")]
        public async Task<IActionResult> LogInAsync(LoginDto loginDto)
        {
            try
            {
                var result = await _service.LogInAsync(loginDto);

                return Ok(result);
            }
            catch (InternalException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUpAsync(SignUpDto signUpDto)
        {
            try
            {
                var result = await _service.SignUpAsync(signUpDto);

                return Ok(result);
            }

            catch (InternalException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
