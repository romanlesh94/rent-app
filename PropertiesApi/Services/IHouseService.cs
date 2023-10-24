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
        Task<List<string>> GetHouseImagesAsync(long houseId);
        Task<string> GetHouseFirstImageAsync(long houseId);
        Task<PagedList<House>> GetHousesByOwnerAsync(long ownerId, PaginationParameters pagination);
        Task<PagedList<GuestBookingDto>> GetBookingsByGuestAsync(long guestId, PaginationParameters pagination);
        Task<PagedList<GuestBookingDto>> GetHistoryByGuestAsync(long guestId, PaginationParameters pagination);
    }
}
