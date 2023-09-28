using HouseApi.Models.Booking;
using HouseApi.Models.Dto;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using HouseApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("getHouses/pageIndex/{pageIndex}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetAllHousesAsync(int pageIndex, int pageSize, [FromQuery] HouseSearchOptions houseSearchOptions)
        {
            var pagination = new PaginationParameters(pageIndex, pageSize);

            var result = await _service.GetAllHousesAsync(pagination, houseSearchOptions);
            return Ok(result);
        }

        [HttpGet("getHouse/id/{id}")]
        public async Task<IActionResult> GetHouseWithPropertiesAsync(int id)
        {
            var result = await _service.GetHouseWithPropertiesAsync(id);
            return Ok(result);
        }

        [HttpGet("getHouseProperties")]
        public async Task<IActionResult> GetHousePropertiesAsync()
        {
            var result = await _service.GetHousePropertiesAsync();
            return Ok(result);
        }

        [HttpPost("createHouse")]
        public async Task<IActionResult> CreateHouseAsync(CreateHouseDto createHouseDto)
        {
            var result = await _service.CreateHouseAsync(createHouseDto);
            return Ok(result);
        }

        [HttpPost("updateHouse")]
        public async Task<IActionResult> UpdateHouseAsync(UpdateHouseDto updateHouseDto)
        {
            await _service.UpdateHouseAsync(updateHouseDto);
            return Ok("The house has been updated!");
        }
        [HttpDelete("deleteHouse")]
        public async Task<IActionResult> DeleteHouseAsync(DeleteHouseDto deleteHouseDto)
        {
            await _service.DeleteHouseAsync(deleteHouseDto);
            return Ok("The house has been deleted!");
        }

        [HttpPost("addHouseBooking")]
        public async Task<IActionResult> AddHouseBookingAsync(HouseBooking houseBooking)
        {
            await _service.AddHouseBookingAsync(houseBooking);
            return Ok("The house has been booked");
        }

        [HttpGet("getHouseBookings")]
        public async Task<IActionResult> GetHouseBookingsAsync(long id)
        {
            var bookings = await _service.GetHouseBookingsAsync(id);
            return Ok(bookings);
        }

    }
}
