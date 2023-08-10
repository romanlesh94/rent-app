using HouseApi.Models;
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

        public async Task<House> CreateHouseAsync(string name, string description, string rules, string address)
        {
            House newHouse = new House
            {
                Name = name,
                Description = description,
                Rules = rules,
                Address = address
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
