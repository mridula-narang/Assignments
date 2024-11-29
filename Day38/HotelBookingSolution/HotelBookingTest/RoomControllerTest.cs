using AutoMapper;
using HotelBookingApp.Contexts;
using HotelBookingApp.Controllers;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Repositories;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Mvc;
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
    public class RoomControllerTest
    {
        DbContextOptions options;
        HotelContext context;
        RoomRepository repository;
        Moq.Mock<ILogger<RoomRepository>> logger;
        private Moq.Mock<ILogger<RoomController>> loggerController;
        Moq.Mock<IMapper> mapper;
        IRoomService roomService;
        RoomDTO room;
        Room roomEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            logger = new Moq.Mock<ILogger<RoomRepository>>();
            loggerController = new Moq.Mock<ILogger<RoomController>>();
            repository = new RoomRepository(context, logger.Object);
            mapper = new Moq.Mock<IMapper>();
            roomService = new RoomService(repository, mapper.Object);
        }

        [Test]
        public async Task TestAddRoom()
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
            var controller = new RoomController(roomService,loggerController.Object);
            var result = await controller.AddRoom(room);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        //test to delete room
        public async Task TestDeleteRoom()
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
            var controller = new RoomController(roomService, loggerController.Object);
            var result = await controller.AddRoom(room);
            //call delete room method
            var result2 = await controller.DeleteRoom(room.HotelId);
            Assert.IsNotNull(result2);
            var resultObject = result2 as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        public async Task TestGetRooms()
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
            var controller = new RoomController(roomService, loggerController.Object);
            var result = await controller.AddRoom(room);
            //call get all rooms method
            var result2 = await controller.GetRooms();
            Assert.IsNotNull(result2);
            var resultObject = result2 as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        public async Task TestGetRoomById()
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
            var controller = new RoomController(roomService, loggerController.Object);
            var result = await controller.AddRoom(room);
            //call get room by id method
            var result2 = await controller.GetRoomById(room.HotelId);
            Assert.IsNotNull(result2);
            var resultObject = result2 as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
