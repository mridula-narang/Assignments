using HotelBookingApp.Contexts;
using HotelBookingApp.Controllers;
using HotelBookingApp.Interfaces;
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
using static HotelBookingApp.Models.User;

namespace HotelBookingTest
{
    public class UserControllerTest
    {
        private DbContextOptions options;
        private HotelContext context;
        private UserRepository repository;
        private UserService service;
        private UserController controller;
        private Moq.Mock<ILogger<UserController>> loggerController;
        private Moq.Mock<ILogger<UserService>> loggerService;
        private Moq.Mock<ILogger<UserRepository>> loggerRepo;
        //add token service
        private Moq.Mock<ITokenService> tokenService;


        [SetUp]
        public void Setup()
        {
            // In-memory database setup
            options = new DbContextOptionsBuilder<HotelContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            context = new HotelContext(options);

            // Mocks for logging
            loggerController = new Moq.Mock<ILogger<UserController>>();
            loggerService = new Moq.Mock<ILogger<UserService>>();
            loggerRepo = new Moq.Mock<ILogger<UserRepository>>();

            // Repository and Service setup
            repository = new UserRepository(context, loggerRepo.Object);
            tokenService = new Moq.Mock<ITokenService>();

            // Controller setup
            service = new UserService(repository, tokenService.Object, loggerService.Object);
            controller = new UserController(loggerController.Object, service);
        }

        [Test]
        // [TestCase("testuser1", "password1", Roles.User)]
        [TestCase("adminuser", "password2", Roles.Admin,"test@gmail.com")]
        public async Task RegisterShouldReturnOk(string username, string password, Roles role,string email)
        {
            // Arrange
            var UserCreateDTO = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Email = email,
                Role = role
            };

            // Act
            var result = await controller.Register(UserCreateDTO);
            Assert.IsNotNull(result);
            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase("adminuser", "password2", Roles.Admin)]
        public async Task LoginShouldReturnOk(string username, string password, Roles role)
        {
            // Arrange
            var user = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = role
            };

            // Register user first (as a prerequisite)
            var result1 = await controller.Register(user);
            Assert.IsNotNull(result1);

            var loginRequest = new LoginRequestDTO
            {
                Username = username,
                Password = password
            };

            var result2 = await controller.Login(loginRequest);
            Assert.IsNotNull(result2);

            // Assert
            var okResult = result2.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
