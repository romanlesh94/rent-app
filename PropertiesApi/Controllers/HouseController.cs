using HouseApi.Models;
using HouseApi.Models.Booking;
using HouseApi.Models.Dto;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using HouseApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> GetAllHousesAsync(int pageIndex, int pageSize, [FromHeader] HouseSearchOptions houseSearchOptions)
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
        [HttpDelete("deleteHouse/{houseId}")]
        public async Task<IActionResult> DeleteHouseAsync(long houseId)
        {
            await _service.DeleteHouseAsync(houseId);
            return Ok("The house has been deleted!");
        }

        [HttpPost("addHouseBooking")]
        public async Task<IActionResult> AddHouseBookingAsync(AddHouseBookingDto houseBooking)
        {
            await _service.AddHouseBookingAsync(houseBooking);
            return Ok("The house has been booked");
        }

        [HttpDelete("cancelBooking/{bookingId}")]
        public async Task<IActionResult> CancelBookingAsync(long bookingId)
        {
            await _service.DeleteBookingAsync(bookingId);
            return Ok("The booking has been canceled");
        }

        [HttpGet("getHouseBookings")]
        public async Task<IActionResult> GetHouseBookingsAsync(long id)
        {
            var bookings = await _service.GetHouseBookingsAsync(id);
            return Ok(bookings);
        }

        [HttpPost("addHouseImage/{houseId}")]
        public async Task<IActionResult> AddHouseImageAsync(long houseId, IFormFile file)
        {
            await _service.AddHouseImageAsync(houseId, file);
            return Ok("The image has been added!");
        }

        [HttpGet("getHouseImages/{houseId}")]
        public async Task<IActionResult> GetHouseImagesAsync(long houseId)
        {
            var result = await _service.GetHouseImagesAsync(houseId);

            /*var fileImages = new List<FileContentResult>();

            foreach(var image in images)
            {
                fileImages.Add(File(image, "image/jpeg"));
            }*/

            return Ok(result);
        }

        [HttpGet("getHouseFirstImage/{houseId}")]
        public async Task<IActionResult> GetHouseFirstImage(long houseId)
        {
            var result = await _service.GetHouseFirstImageAsync(houseId);
            return Ok(result);
        }

        [HttpGet("getHousesByOwner/{ownerId}/pageIndex/{pageIndex}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetHousesByOwnerAsync(long ownerId, int pageIndex, int pageSize)
        {
            var pagination = new PaginationParameters(pageIndex, pageSize);
            
            var result = await _service.GetHousesByOwnerAsync(ownerId, pagination);
            return Ok(result);
        }

        [HttpGet("getBookingsByGuest/{guestId}/pageIndex/{pageIndex}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetBookingsByGuestAsync(long guestId, int pageIndex, int pageSize)
        {
            var pagination = new PaginationParameters(pageIndex, pageSize);

            var result = await _service.GetBookingsByGuestAsync(guestId, pagination);
            return Ok(result);
        }

        [HttpGet("getHistoryByGuest/{guestId}/pageIndex/{pageIndex}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetHistoryByGuestAsync(long guestId, int pageIndex, int pageSize)
        {
            var pagination = new PaginationParameters(pageIndex, pageSize);

            var result = await _service.GetHistoryByGuestAsync(guestId, pagination);
            return Ok(result);
        }
    }
}
