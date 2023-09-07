using HouseApi.Models;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Repository
{
    public interface IHouseRepository
    {
        Task AddHouseAsync(House house);
        Task<List<House>> GetHousesAsync();
        Task<(List<House> houses, int notPagedCount)> GetHousesPageAsync(PaginationParameters pagination, HouseSearchOptions houseSearchOptions);
        Task<House> GetHouseAsync(string name);
        Task<House> GetHouseByIdAsync(long id);
        Task UpdateHouseAsync(House house);
        Task DeleteHouseAsync(House house);
    }
}
