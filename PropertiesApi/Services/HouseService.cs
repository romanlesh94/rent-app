using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using HouseApi.Entities.Exceptions;
using HouseApi.Models;
using HouseApi.Models.Booking;
using HouseApi.Models.Dto;
using HouseApi.Models.Exceptions;
using HouseApi.Models.Options;
using HouseApi.Models.Pagination;
using HouseApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace HouseApi.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _config;

        public HouseService(IHouseRepository houseRepository, IImageRepository imageRepository, IConfiguration config)
        {
            _houseRepository = houseRepository;
            _imageRepository = imageRepository;
            _config = config;
        }

        private const long FileMaxSize = 5242880;

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
                OwnerId = createHouseDto.OwnerId,
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

            var house = await _houseRepository.GetHouseByIdAsync(updateHouseDto.Id);

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

        public async Task DeleteHouseAsync(long houseId)
        {
            var house = await _houseRepository.GetHouseByIdAsync(houseId);

            if (house == null)
            {
                throw new NotFoundException("House not found!");
            }

            await _houseRepository.DeleteHouseAsync(house);
        }

        public async Task AddHouseBookingAsync(AddHouseBookingDto addHouseBooking)
        {
            string format = "MM/dd/yyyy";
            DateTime checkInDate = DateTime.ParseExact(addHouseBooking.CheckInDate, format, CultureInfo.InvariantCulture);
            DateTime checkOutDate = DateTime.ParseExact(addHouseBooking.CheckOutDate, format, CultureInfo.InvariantCulture);

            //DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)
            HouseBooking houseBooking = new HouseBooking
            {
                HouseId = addHouseBooking.HouseId,
                GuestId = addHouseBooking.GuestId,
                Price = addHouseBooking.Price,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
            };

            await _houseRepository.AddHouseBookingAsync(houseBooking);
        }

        public async Task DeleteBookingAsync(long bookingId)
        {
            var booking = await _houseRepository.GetBookingByIdAsync(bookingId);

            if(booking == null)
            {
                throw new NotFoundException("This booking doesn't exist");
            }

            await _houseRepository.DeleteHouseBookingAsync(booking);
        }

        public async Task<List<HouseBooking>> GetHouseBookingsAsync(long id)
        {
            return await _houseRepository.GetHouseBookingsAsync(id);
        }

        public async Task AddHouseImageAsync(long houseId, IFormFile file)
        {
            var house = await _houseRepository.GetHouseByIdAsync(houseId);

            if (house == null)
            {
                throw new NotFoundException("House is not found!");
            }

            if (file.Length > FileMaxSize)
            {
                throw new InternalException("File size is more than 5mb");
            }

            if (file.Length == 0)
            {
                throw new InternalException("File length is 0");
            }

            var storageClient = await StorageClient.CreateAsync(GoogleCredential.FromFile(_config.GetValue<string>("GoogleCredentialFile")));
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string objectName = $"images/{fileName}";

            using (var imageMemoryStream = new MemoryStream())
            {
                await file.CopyToAsync(imageMemoryStream);
                imageMemoryStream.Position = 0;
                await storageClient.UploadObjectAsync(_config["GoogleCloudStorageBucket"], objectName, null, imageMemoryStream);
            }

            var storageObject = await storageClient.GetObjectAsync(_config["GoogleCloudStorageBucket"], objectName);

            var image = new Image
            {
                House = house,
                ImageUrl = storageObject.MediaLink,
            };

            await _imageRepository.AddHouseImageAsync(image);
        }

        public async Task<List<string>> GetHouseImagesAsync(long houseId)
        {
            var images = await _imageRepository.GetHouseImagesAsync(houseId);

            if(images == null)
            {
                throw new NotFoundException("This house has no images");
            }

            return images;
        }

        public async Task<string> GetHouseFirstImageAsync(long houseId)
        {
            var image = await _imageRepository.GetHouseFirstImageAsync(houseId);

            if(image == null)
            {
                throw new NotFoundException("This house has no images");
            }

            return image.ImageUrl;
        }

        public async Task<PagedList<House>> GetHousesByOwnerAsync(long ownerId, PaginationParameters pagination)
        {
            var houses = await _houseRepository.GetHousesByOwnerAsync(ownerId, pagination);

            return new PagedList<House>(houses.houses, houses.notPagedCount, pagination);
        }

        public async Task<PagedList<GuestBookingDto>> GetBookingsByGuestAsync(long guestId, PaginationParameters pagination)
        {
            var bookings = await _houseRepository.GetBookingsByGuestAsync(guestId, pagination);

            return new PagedList<GuestBookingDto>(bookings.bookings, bookings.notPagedCount, pagination);
        }
        public async Task<PagedList<GuestBookingDto>> GetHistoryByGuestAsync(long guestId, PaginationParameters pagination)
        {
            var bookings = await _houseRepository.GetHistoryByGuestAsync(guestId, pagination);

            return new PagedList<GuestBookingDto>(bookings.bookings, bookings.notPagedCount, pagination);
        }

    }
}
