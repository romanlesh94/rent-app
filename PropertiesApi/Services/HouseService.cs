using HouseApi.Entities.Exceptions;
using HouseApi.Models;
using HouseApi.Models.Dto;
using HouseApi.Models.Exceptions;
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
            var house = await _houseRepository.GetHouseAsync(createHouseDto.Name);

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
            
            return newHouse;
        }

        public async Task<PagedList<House>> GetAllHousesAsync(PaginationParameters pagination)
        {
            var houses = await _houseRepository.GetHousesPageAsync(pagination);

            return new PagedList<House>(houses.houses, houses.notPagedCount, pagination);
        }

        public async Task UpdateHouseAsync(UpdateHouseDto updateHouseDto)
        {

            var house = await _houseRepository.GetHouseAsync(updateHouseDto.Name);

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

        public async Task DeleteHouseAsync(DeleteHouseDto deleteHouseDto)
        {
            var house = await _houseRepository.GetHouseAsync(deleteHouseDto.Name);

            if (house == null)
            {
                throw new NotFoundException("House not found!");
            }

            await _houseRepository.DeleteHouseAsync(house);
        }
    }
}
