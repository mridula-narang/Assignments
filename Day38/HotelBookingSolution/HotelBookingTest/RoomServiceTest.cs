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
using static HotelBookingApp.Models.Room;

namespace HotelBookingTest
{
    public class RoomServiceTest
    {
        DbContextOptions options;
        HotelContext context;
        RoomRepository repository;
        Moq.Mock<ILogger<RoomRepository>> logger;
        Moq.Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            logger = new Moq.Mock<ILogger<RoomRepository>>();
            mapper = new Moq.Mock<IMapper>();
            repository = new RoomRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            var room = new RoomDTO
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };

            var roomEntity = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            mapper.Setup(m => m.Map<Room>(room)).Returns(roomEntity);
            IRoomService service = new RoomService(repository, mapper.Object);
            var addedRoom = await service.AddRoom(room);
            Assert.IsNotNull(addedRoom);
        }

        [Test]
        public async Task TestDelete()
        {
            var room = new RoomDTO
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };

            var roomEntity = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            mapper.Setup(m => m.Map<Room>(room)).Returns(roomEntity);
            IRoomService service = new RoomService(repository, mapper.Object);
            var addedRoom = await service.AddRoom(room);
            var deletedRoom = await service.DeleteRoom(addedRoom.RoomId);
            Assert.IsNotNull(deletedRoom);
        }

        [Test]
        public async Task TestGet()
        {
            var room = new RoomDTO
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };

            var roomEntity = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            mapper.Setup(m => m.Map<Room>(room)).Returns(roomEntity);
            IRoomService service = new RoomService(repository, mapper.Object);
            var addedRoom = await service.AddRoom(room);
            var getRoom = await service.GetRoom(addedRoom.RoomId);
            Assert.AreEqual(getRoom.Name, addedRoom.Name);
        }

        [Test]
        public async Task TestGetAll()
        {
            var room = new RoomDTO
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };

            var roomEntity = new Room
            {
                Type = RoomType.Single,
                Price = 5000,
                IsBooked = RoomStatus.Available,
                Name = "Test name",
                Features = "Test features",
                Capacity = 2,
                HotelId = 1
            };
            mapper.Setup(m => m.Map<Room>(room)).Returns(roomEntity);
            IRoomService service = new RoomService(repository, mapper.Object);
            var addedRoom = await service.AddRoom(room);
            var rooms = await service.GetAllRooms();
            Assert.IsTrue(rooms.Count() > 0);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
