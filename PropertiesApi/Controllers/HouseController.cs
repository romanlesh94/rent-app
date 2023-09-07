﻿using HouseApi.Models.Dto;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
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

        [HttpGet("getHouses/pageIndex/{pageIndex}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetAllHousesAsync(int pageIndex, int pageSize, [FromQuery] HouseSearchOptions houseSearchOptions)
        {
            var pagination = new PaginationParameters(pageIndex, pageSize);

            var result = await _service.GetAllHousesAsync(pagination, houseSearchOptions);
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



    }
}
