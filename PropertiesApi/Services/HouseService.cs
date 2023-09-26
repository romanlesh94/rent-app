using HouseApi.Entities.Exceptions;
using HouseApi.Models;
using HouseApi.Models.Dto;
using HouseApi.Models.Exceptions;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
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
            var house = await _houseRepository.GetHouseByNameAsync(createHouseDto.Name);

            if (house != null) 
            {
                throw new InternalException("This name already exists!");
            }

            House newHouse = new House
            {
                Name = createHouseDto.Name.Trim(),
                Description = createHouseDto.Description.Trim(),
                Rules = createHouseDto.Rules.Trim(),
                Address = createHouseDto.Address.Trim(),
                Price = createHouseDto.Price,
            };

            await _houseRepository.AddHouseAsync(newHouse);

            var createdHouse = await _houseRepository.GetHouseByNameAsync(createHouseDto.Name);

            foreach (var property in createHouseDto.Properties)
            {
                HousePropertyMapping houseProperty = new HousePropertyMapping
                {
                    HouseId = createdHouse.Id,
                    PropertyId = property,
                };

                await _houseRepository.AddHousePropertyAsync(houseProperty);
            }
            
            return newHouse;
        }

        public async Task<PagedList<House>> GetAllHousesAsync(PaginationParameters pagination, HouseSearchOptions houseSearchOptions)
        {
            var houses = await _houseRepository.GetHousesPageAsync(pagination, houseSearchOptions);

            return new PagedList<House>(houses.houses, houses.notPagedCount, pagination);
        }

        public async Task UpdateHouseAsync(UpdateHouseDto updateHouseDto)
        {

            var house = await _houseRepository.GetHouseByNameAsync(updateHouseDto.Name);

            if (house == null)
            {
                throw new NotFoundException("House not found!");
            }

            house.Name = updateHouseDto.Name.Trim();
            house.Description = updateHouseDto.Description.Trim();
            house.Rules = updateHouseDto.Rules.Trim();
            house.Address = updateHouseDto.Address.Trim();
            house.Price = updateHouseDto.Price;

            await _houseRepository.UpdateHouseAsync(house);
        }

        public async Task<List<Property>> GetHousePropertiesAsync()
        {
            return await _houseRepository.GetHousePropertiesAsync();
        }

        public async Task<HousePageDto> GetHouseWithPropertiesAsync(long id)
        {
            var house = await _houseRepository.GetHouseByIdAsync(id);

            var properties = await _houseRepository.GetHousePropertiesAsync(id);

            return new HousePageDto (house, properties);
        }

        public async Task DeleteHouseAsync(DeleteHouseDto deleteHouseDto)
        {
            var house = await _houseRepository.GetHouseByNameAsync(deleteHouseDto.Name);

            if (house == null)
            {
                throw new NotFoundException("House not found!");
            }

            await _houseRepository.DeleteHouseAsync(house);
        }
    }
}
