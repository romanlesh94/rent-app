using HouseApi.Models;
using Microsoft.EntityFrameworkCore;
using HouseApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseApi.Models.Pagination;
using HouseApi.Helpers;

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

        public async Task<House> GetHouseAsync(string name)
        {
            return await _context.Houses.FirstOrDefaultAsync(x => x.Name == name.Trim());
        }

        public async Task<House> GetHouseByIdAsync(long id)
        {
            return await _context.Houses.FindAsync(id);
        }

        public async Task<(List<House> houses, int notPagedCount)> GetHousesPageAsync(PaginationParameters pagination)
        {
            var query = _context.Houses.AsQueryable();

            return (await PaginationHelper.GetPagedListAsync(query, pagination), await query.CountAsync());
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

    }
}
