using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;

namespace rent.Controllers
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

        [HttpPost("/log-in")]
        public async Task<IActionResult> LogInAsync(string login, string password)
        {
            var result = await _service.LogInAsync(login, password);

            return Ok(result);
        }

        [HttpPost("/sign-up")]
        public async Task<IActionResult> SignUpAsync(string login, string password, string email, string country)
        {
            await _service.SignUpAsync(login, password, email, country);

            return Ok("You have signed up!");

            /*var result = await _service.LogInAsync(login, password);

            return Ok(result);*/
        }

    }
}
