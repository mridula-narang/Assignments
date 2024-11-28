using AutoMapper;
using HotelBookingApp.Contexts;
using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HotelBookingApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingTest
{
    public class HotelRepositoryTest
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
            var hotel = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var addedHotel = await repository.Add(hotel);
            Assert.IsTrue(addedHotel.HotelId == hotel.HotelId);
        }

        [Test]
        public async Task TestDelete()
        {
            var hotel = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var addedHotel = await repository.Add(hotel);
            var deletedHotel = await repository.Delete(addedHotel.HotelId);
            Assert.IsTrue(deletedHotel.HotelId == addedHotel.HotelId);
        }

        [Test]
        public async Task TestGet()
        {
            var hotel = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var addedHotel = await repository.Add(hotel);
            var getHotel = await repository.Get(addedHotel.HotelId);
            Assert.IsTrue(getHotel.HotelId == addedHotel.HotelId);
        }

        [Test]
        public async Task TestGetAll()
        {
            var hotel = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var addedHotel = await repository.Add(hotel);
            var hotels = await repository.GetAll();
            Assert.IsTrue(hotels.Count() == 1);
        }

        [Test]
        public async Task TestUpdate()
        {
            var hotel = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var addedHotel = await repository.Add(hotel);
            var updatedHotel = await repository.Update(addedHotel.HotelId, new Hotel
            {
                Location = "updated location",
                Name = "updated name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            });
            Assert.IsTrue(updatedHotel.Location == "updated location");
        }

        [Test]
        public async Task TestUpdateThrowsNotFoundException()
        {
            var hotel = new Hotel
            {
                Location = "test location",
                Name = "test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };
            var addedHotel = await repository.Add(hotel);
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(addedHotel.HotelId + 1, new Hotel
            {
                Location = "updated location",
                Name = "updated name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            }));
        }

        [Test]
        public async Task NotFoundException()
        {
            // Assert
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999)));
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(2)));
        }

        [Test]
        public async Task CollectionEmptyException()
        {
            // Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
