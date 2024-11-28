using AutoMapper;
using HotelBookingApp.Contexts;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Repositories;
using HotelBookingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingTest
{
    public class HotelServiceTest
    {
        DbContextOptions options;
        HotelContext context;
        HotelRepository repository;
        Moq.Mock<ILogger<HotelRepository>> logger;
        Moq.Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            logger = new Moq.Mock<ILogger<HotelRepository>>();
            mapper = new Moq.Mock<IMapper>();
            repository = new HotelRepository(context, mapper.Object, logger.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            var hotel = new HotelDTO
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var hotelEntity = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            IHotelService service = new HotelService(repository, mapper.Object);
            var addedHotel = await service.AddHotel(hotel);
            Assert.IsTrue(addedHotel.HotelId == hotelEntity.HotelId);
        }

        [Test]
        public async Task TestDelete()
        {
            var hotel = new HotelDTO
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var hotelEntity = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            IHotelService service = new HotelService(repository, mapper.Object);
            var addedHotel = await service.AddHotel(hotel);
            var deletedHotel = await service.DeleteHotel(addedHotel.HotelId);
            Assert.IsTrue(deletedHotel.HotelId == addedHotel.HotelId);
        }

        [Test]
        public async Task TestGet()
        {
            var hotel = new HotelDTO
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var hotelEntity = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            IHotelService service = new HotelService(repository, mapper.Object);
            var addedHotel = await service.AddHotel(hotel);
            var getHotel = await service.GetHotel(addedHotel.HotelId);
            Assert.IsTrue(getHotel.HotelId == addedHotel.HotelId);
        }

        [Test]
        public async Task TestGetAll()
        {
            var hotel = new HotelDTO
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var hotelEntity = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            IHotelService service = new HotelService(repository, mapper.Object);
            var addedHotel = await service.AddHotel(hotel);
            var getHotels = await service.GetAllHotel();
            Assert.IsTrue(getHotels.Count() > 0);
        }

        [Test]
        public async Task TestUpdateHotelCheckIn()
        {
            var hotel = new HotelDTO
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var hotelEntity = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            IHotelService service = new HotelService(repository, mapper.Object);
            var addedHotel = await service.AddHotel(hotel);
            var updatedHotel = await service.UpdateHotelCheckIn(hotelEntity.HotelId, new TimeSpan(15, 0, 0));
            Assert.IsTrue(updatedHotel.StandardCheckIn == new TimeSpan(15, 0, 0));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

    }
}
