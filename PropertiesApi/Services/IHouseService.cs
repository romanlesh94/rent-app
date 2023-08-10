using HouseApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public interface IHouseService
    {
        Task<House> CreateHouseAsync(string name, string description, string rules, string address);
        Task<List<House>> GetAllHousesAsync();
    }
}
