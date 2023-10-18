using HouseApi.Models;
using HouseApi.Models.Booking;
using HouseApi.Models.Dto;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseApi.Repository
{
    public interface IHouseRepository
    {
        Task AddHouseAsync(House house);
        Task<List<House>> GetHousesAsync();
        Task<(List<House> houses, int notPagedCount)> GetHousesPageAsync(PaginationParameters pagination, HouseSearchOptions houseSearchOptions);
        Task<House> GetHouseByNameAsync(string name);
        Task<House> GetHouseByIdAsync(long id);
        Task UpdateHouseAsync(House house);
        Task DeleteHouseAsync(House house);
        Task<IEnumerable<HousePropertyDto>> GetHousePropertiesAsync(long id);
        Task<List<Property>> GetHousePropertiesAsync();
        Task AddHousePropertyAsync(HousePropertyMapping property);
        Task AddHouseBookingAsync(HouseBooking houseBooking);
        Task DeleteHouseBookingAsync(HouseBooking booking);
        Task<HouseBooking> GetBookingByIdAsync(long bookingId);
        Task<List<HouseBooking>> GetHouseBookingsAsync(long id);
        Task<List<House>> GetHousesByOwnerAsync(long ownerId);
        Task<List<GuestBookingDto>> GetBookingsByGuestAsync(long guestId);
    }
}
