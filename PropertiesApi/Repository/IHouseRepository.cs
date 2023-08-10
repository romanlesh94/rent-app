using HouseApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Repository
{
    public interface IHouseRepository
    {
        Task AddHouseAsync(House house);
        Task<List<House>> GetHousesAsync();
        Task<House> GetHouseByIdAsync(long id);
        Task UpdateHouseAsync(House house);
        Task DeleteHouseAsync(House house);
    }
}
