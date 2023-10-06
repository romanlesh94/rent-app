using HouseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseApi.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly HouseDbContext _context;

        public ImageRepository(HouseDbContext context)
        {
            _context = context;
        }

        public async Task AddHouseImageAsync(Image image)
        {
            await _context.Images.AddAsync(image);

            await _context.SaveChangesAsync();
        }

        public async Task<Image> GetHouseFirstImageAsync(long houseId)
        {
            return await _context.Images
                .FirstOrDefaultAsync(i => i.HouseId == houseId);
        }

        public async Task<List<byte[]>> GetHouseImagesAsync(long houseId)
        {
            return await _context.Images
                .Where(i => i.HouseId == houseId)
                .Select(i => i.Data)
                .ToListAsync();
        }
    }
}
