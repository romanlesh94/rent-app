using HouseApi.Models;
using HouseApi.Models.Dto;
using HouseApi.Models.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public interface IHouseService
    {
        Task<House> CreateHouseAsync(CreateHouseDto createHouseDto);
        Task<PagedList<House>> GetAllHousesAsync(PaginationParameters pagination);
        Task UpdateHouseAsync(UpdateHouseDto updateHouseDto);
        Task DeleteHouseAsync(DeleteHouseDto deleteHouseDto);
    }
}
