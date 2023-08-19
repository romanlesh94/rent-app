using HouseApi.Models.Dto;
using HouseApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HouseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HouseController : Controller
    {
        private readonly IHouseService _service;

        public HouseController(IHouseService service)
        {
            _service = service;
        }

        [HttpGet("/get-houses")]
        public async Task<IActionResult> GetAllHousesAsync()
        {
            var result = await _service.GetAllHousesAsync();
            return Ok(result);
        }

        [HttpPost("/create-house")]
        public async Task<IActionResult> CreateHouseAsync(CreateHouseDto createHouseDto)
        {
            var result = await _service.CreateHouseAsync(createHouseDto);
            return Ok(result);
        }

    }
}
