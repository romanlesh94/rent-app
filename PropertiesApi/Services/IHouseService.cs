using HouseApi.Models;
using HouseApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public interface IHouseService
    {
        Task<House> CreateHouseAsync(CreateHouseDto createHouseDto);
        Task<List<House>> GetAllHousesAsync();
        Task UpdateHouseAsync(UpdateHouseDto updateHouseDto);
        Task DeleteHouseAsync(DeleteHouseDto deleteHouseDto);
    }
}
