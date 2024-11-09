using HotelBookingApp.Contexts;
using HotelBookingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.Models;
using static HotelBookingApp.Models.Room;
using AutoMapper.Features;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using HotelBookingApp.Exceptions;

namespace HotelBookingTest
{
    public class RoomRepositoryTest
    {
        DbContextOptions options;
        HotelContext context;
        RoomRepository repository;
        Moq.Mock<ILogger<RoomRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            logger = new Moq.Mock<ILogger<RoomRepository>>();
            repository = new RoomRepository(context, logger.Object);
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        [TestCase(1, 200, 1, "Room2", "TV, AC, Fridge", 4, 2)]
        [TestCase(2, 300, 0, "Room3", "TV, AC, Fridge, Balcony", 6, 3)]
        public async Task TestAdd(RoomType roomType,decimal price, RoomStatus isBooked, string name, string features,int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = name,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            var addedRoom = await repository.Add(room);
            Assert.IsTrue(addedRoom.RoomId == room.RoomId);
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        [TestCase(1, 200, 1, "Room2", "TV, AC, Fridge", 4, 2)]
        [TestCase(2, 300, 0, "Room3", "TV, AC, Fridge, Balcony", 6, 3)]
        public async Task TestDelete(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = name,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            var addedRoom = await repository.Add(room);
            var deletedRoom = await repository.Delete(addedRoom.RoomId);
            Assert.IsTrue(deletedRoom.RoomId == addedRoom.RoomId);
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        [TestCase(1, 200, 1, "Room2", "TV, AC, Fridge", 4, 2)]
        [TestCase(2, 300, 0, "Room3", "TV, AC, Fridge, Balcony", 6, 3)]

        public async Task TestGet(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = name,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            var addedRoom = await repository.Add(room);
            var getRoom = await repository.Get(addedRoom.RoomId);
            Assert.IsTrue(getRoom.RoomId == addedRoom.RoomId);
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        [TestCase(1, 200, 1, "Room2", "TV, AC, Fridge", 4, 2)]
        [TestCase(2, 300, 0, "Room3", "TV, AC, Fridge, Balcony", 6, 3)]
        public async Task TestUpdate(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = name,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            var addedRoom = await repository.Add(room);
            addedRoom.Price = 500;
            var updatedRoom = await repository.Update(addedRoom.RoomId, addedRoom);
            Assert.IsTrue(updatedRoom.Price == 500);
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        [TestCase(1, 200, 1, "Room2", "TV, AC, Fridge", 4, 2)]
        [TestCase(2, 300, 0, "Room3", "TV, AC, Fridge, Balcony", 6, 3)]

        public async Task TestGetAll(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = name,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            await repository.Add(room);
            var rooms = await repository.GetAll();
            Assert.IsTrue(rooms.Any(p => p.Type == roomType && p.Price == price && p.IsBooked == isBooked && p.Name == name && p.Features == features && p.HotelId == hotelId));
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        [TestCase(1, 200, 1, "Room2", "TV, AC, Fridge", 4, 2)]
        [TestCase(2, 300, 0, "Room3", "TV, AC, Fridge, Balcony", 6, 3)]

        public async Task TestGetAllEmpty(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = name,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            var rooms = await repository.GetAll();
            Assert.IsTrue(rooms.Count() == 0);
        }

        [Test]
        public async Task NotFoundException()
        {
            // Assert
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999)));
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(2)));
        }

        [Test]
        [TestCase(0, 100, 0, "Room1", "TV, AC", 2, 1)]
        public async Task CollectionEmptyException(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            // Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        [TestCase(0, 100, 0, "", "TV, AC", 2, 1)]
        public async Task CouldNotAddException(RoomType roomType, decimal price, RoomStatus isBooked, string name, string features, int capacity, int hotelId)
        {
            var room = new Room
            {
                Type = roomType,
                Price = price,
                IsBooked = isBooked,
                Name = null,
                Features = features,
                Capacity = capacity,
                HotelId = hotelId
            };
            // Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(room));
        }


        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
