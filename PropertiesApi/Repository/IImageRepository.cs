using HouseApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Repository
{
    public interface IImageRepository
    {
        Task AddHouseImageAsync(Image image);
        Task<Image> GetHouseFirstImageAsync(long houseId);
        Task<List<byte[]>> GetHouseImagesAsync(long houseId);
    }
}
