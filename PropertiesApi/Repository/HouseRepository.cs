using HouseApi.Models;
using Microsoft.EntityFrameworkCore;
using HouseApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseApi.Models.Pagination;
using HouseApi.Helpers;
using HouseApi.Models.Options;
using HouseApi.Models.Dto;
using HouseApi.Models.Booking;
using HouseApi.Entities.Exceptions;

namespace HouseApi.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext _context;

        public HouseRepository(HouseDbContext context)
        {
            _context = context;
        }

        public async Task AddHouseAsync(House house)
        {
            await _context.Houses.AddAsync(house);
            await _context.SaveChangesAsync();
        }

        public async Task<List<House>> GetHousesAsync()
        {
            return await _context.Houses.ToListAsync();
        }

        public async Task<House> GetHouseByNameAsync(string name)
        {
            return await _context.Houses.FirstOrDefaultAsync(x => x.Name == name.Trim());
        }

        public async Task AddHousePropertyAsync(HousePropertyMapping property)
        {
            await _context.HousePropertyMappings.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task<House> GetHouseByIdAsync(long id)
        {
            return await _context.Houses.FindAsync(id);
        }

        public async Task<List<Property>> GetHousePropertiesAsync()
        {
            return await _context.HouseProperties.ToListAsync();
        }

        public async Task <IEnumerable<HousePropertyDto>> GetHousePropertiesAsync(long id)
        {
            var mappings = _context.HousePropertyMappings
                .Where(x => x.HouseId == id);

            var properties = _context.HouseProperties;

            var houseProperties = properties.Join(mappings,
                    p => p.Id,
                    m => m.PropertyId,
                    (p, m) => new HousePropertyDto{ Id = m.PropertyId, Text = p.PropertyText }
                );

            return await houseProperties.ToListAsync();
        }

        public async Task<(List<House> houses, int notPagedCount)> GetHousesPageAsync(PaginationParameters pagination, 
            HouseSearchOptions houseSearchOptions = null)
        {
            var query = _context.Houses.AsQueryable().OrderBy(h => h.Name);

            if (houseSearchOptions == null)
            {
                return (await PaginationHelper.GetPagedListAsync(query, pagination), await query.CountAsync());
            }

            var searchQuery = SearchHelper.BuildSearchQuery(query, houseSearchOptions);

            return (await PaginationHelper.GetPagedListAsync(searchQuery, pagination), await searchQuery.CountAsync());
        }

        public async Task UpdateHouseAsync(House house)
        {
            _context.Entry(house).State = EntityState.Modified;
            _context.Houses.Update(house);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHouseAsync(House house)
        {
            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();
        }

        public async Task AddHouseBookingAsync(HouseBooking houseBooking)
        {
            var newBooking = new HouseBooking
            {
                Id = houseBooking.Id,
                HouseId = houseBooking.HouseId,
                GuestId = houseBooking.GuestId,
                Price = houseBooking.Price,
                CheckInDate = houseBooking.CheckInDate.Date,
                CheckOutDate = houseBooking.CheckOutDate.Date,
            };

            var oldBooking = await _context.Bookings.AnyAsync(b => b.HouseId == newBooking.HouseId &&
                                                                b.CheckInDate < newBooking.CheckOutDate &&
                                                                b.CheckOutDate > newBooking.CheckInDate);

            if (oldBooking)
            {
                throw new InternalException("The house is already booked on these days");
            }
            
            await _context.Bookings.AddAsync(newBooking);
            await _context.SaveChangesAsync();
        }

        public async Task<HouseBooking> GetBookingByIdAsync(long bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }
        public async Task DeleteHouseBookingAsync(HouseBooking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HouseBooking>> GetHouseBookingsAsync(long id)
        {
            return await _context.Bookings.Where(b => b.HouseId == id).ToListAsync();
        }

        public async Task <List<House>> GetHousesByOwnerAsync(long ownerId)
        {
            return await _context.Houses.Where(h => h.OwnerId == ownerId).ToListAsync();
        }


        public async Task<List<GuestBookingDto>> GetBookingsByGuestAsync(long guestId)
        {
            var houses = _context.Houses;
            var bookings = _context.Bookings
                .Join(houses,
                    b => b.HouseId,
                    h => h.Id,
                    (b, h) => new GuestBookingDto
                    {
                        Id = b.Id,
                        CheckInDate = b.CheckInDate,
                        CheckOutDate = b.CheckOutDate,
                        GuestId = b.GuestId,
                        HouseId = b.HouseId,
                        Price = b.Price,
                        HouseName = h.Name,
                        HouseAddress = h.Address
                    }
                )
                .Where(b => b.GuestId == guestId);

            return await bookings.ToListAsync();
        }
    }
}
