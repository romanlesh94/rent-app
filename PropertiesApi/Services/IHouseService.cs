using HouseApi.Models;
using HouseApi.Models.Booking;
using HouseApi.Models.Dto;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public interface IHouseService
    {
        Task<House> CreateHouseAsync(CreateHouseDto createHouseDto);
        Task<PagedList<House>> GetAllHousesAsync(PaginationParameters pagination, HouseSearchOptions houseSearchOptions);
        Task UpdateHouseAsync(UpdateHouseDto updateHouseDto);
        Task DeleteHouseAsync(DeleteHouseDto deleteHouseDto);
        Task<HousePageDto> GetHouseWithPropertiesAsync(long id);
        Task<List<Property>> GetHousePropertiesAsync();
        Task AddHouseBookingAsync(AddHouseBookingDto addHouseBooking);
        Task<List<HouseBooking>> GetHouseBookingsAsync(long id);
        Task AddHouseImageAsync(long houseId, IFormFile file);
        Task<List<byte[]>> GetHouseImagesAsync(long houseId);
        Task<byte[]> GetHouseFirstImageAsync(long houseId);
    }
}
