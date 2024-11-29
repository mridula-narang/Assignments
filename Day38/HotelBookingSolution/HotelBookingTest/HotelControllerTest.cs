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

namespace HotelBookingTest
{
    public class HotelControllerTest
    {
        DbContextOptions options;
        HotelContext context;
        HotelRepository repository;
        Moq.Mock<ILogger<HotelRepository>> logger;
        private Moq.Mock<ILogger<HotelController>> loggerController;
        Moq.Mock<IMapper> mapper;
        IHotelService hotelService;
        HotelDTO hotel;
        Hotel hotelEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            logger = new Moq.Mock<ILogger<HotelRepository>>();
            loggerController = new Moq.Mock<ILogger<HotelController>>();
            mapper = new Moq.Mock<IMapper>();
            repository = new HotelRepository(context, mapper.Object, logger.Object);
            hotelService = new HotelService(repository, mapper.Object);
        }

        [Test]
        public async Task TestAddHotel()
        {
            var hotel = new HotelDTO
            {
                Location = "Test location",
                Name = "Test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };

            var hotelEntity = new Hotel
            {
                Location = "Test location",
                Name = "Test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };

            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            var controller = new HotelController(hotelService, loggerController.Object);
            var result = await controller.AddHotel(hotel);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        [Test]
        public async Task TestGetHotel()
        {
            var hotel = new HotelDTO
            {
                Location = "Test location",
                Name = "Test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };

            var hotelEntity = new Hotel
            {
                Location = "Test location",
                Name = "Test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };

            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            var controller = new HotelController(hotelService, loggerController.Object);
            var result = await controller.AddHotel(hotel);
            var result2 = await controller.GetHotelById(1);
            Assert.IsNotNull(result2);
            var resultObject = result2 as OkObjectResult;
            Assert.IsNotNull(resultObject);
        }

        [Test]
        public async Task TestGetHotels()
        {
            var hotel = new HotelDTO
            {
                Location = "Test location",
                Name = "Test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };

            var hotelEntity = new Hotel
            {
                Location = "Test location",
                Name = "Test name",
                StandardCheckIn = new TimeSpan(14, 0, 0),
                StandardCheckOut = new TimeSpan(12, 0, 0),
            };

            mapper.Setup(m => m.Map<Hotel>(hotel)).Returns(hotelEntity);
            var controller = new HotelController(hotelService, loggerController.Object);
            var result = await controller.AddHotel(hotel);
            var result2 = await controller.GetHotels();
            Assert.IsNotNull(result2);
            var resultObject = result2 as OkObjectResult;
            Assert.IsNotNull(resultObject);
        }




        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
