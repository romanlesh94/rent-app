using HouseApi.Models;
using HouseApi.Models.Dto;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public interface IHouseService
    {
        Task<House> CreateHouseAsync(CreateHouseDto createHouseDto);
        Task<PagedList<House>> GetAllHousesAsync(PaginationParameters pagination, HouseSearchOptions houseSearchOptions);
        Task UpdateHouseAsync(UpdateHouseDto updateHouseDto);
        Task DeleteHouseAsync(DeleteHouseDto deleteHouseDto);
    }
}
