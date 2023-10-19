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
        Task DeleteHouseAsync(long houseId);
        Task<HousePageDto> GetHouseWithPropertiesAsync(long id);
        Task<List<Property>> GetHousePropertiesAsync();
        Task AddHouseBookingAsync(AddHouseBookingDto addHouseBooking);
        Task<List<HouseBooking>> GetHouseBookingsAsync(long id);
        Task DeleteBookingAsync(long bookingId);
        Task AddHouseImageAsync(long houseId, IFormFile file);
        Task<List<byte[]>> GetHouseImagesAsync(long houseId);
        Task<byte[]> GetHouseFirstImageAsync(long houseId);
        Task<List<House>> GetHousesByOwnerAsync(long ownerId);
        Task<List<GuestBookingDto>> GetBookingsByGuestAsync(long guestId);
        Task<List<GuestBookingDto>> GetHistoryByGuestAsync(long guestId);
    }
}
