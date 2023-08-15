﻿using HouseApi.Models;
using Microsoft.EntityFrameworkCore;
using HouseApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<House> GetHouseByIdAsync(long id)
        {
            return await _context.Houses.FindAsync(id);
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