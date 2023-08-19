using HouseApi.Models;
using HouseApi.Models.Dto;
using HouseApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<House> CreateHouseAsync(CreateHouseDto createHouseDto)
        {
            House newHouse = new House
            {
                Name = createHouseDto.Name,
                Description = createHouseDto.Description,
                Rules = createHouseDto.Rules,
                Address = createHouseDto.Address
            };

            await _houseRepository.AddHouseAsync(newHouse);
            
            return newHouse;
        }

        public async Task<List<House>> GetAllHousesAsync()
        {
            var houses = await _houseRepository.GetHousesAsync();

            return houses;
        }
    }
}
