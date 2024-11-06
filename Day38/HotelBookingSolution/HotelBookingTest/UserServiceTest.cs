using HotelBookingApp.Contexts;
using HotelBookingApp.Repositories;
using HotelBookingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using HotelBookingApp.Models.DTOs;
using static HotelBookingApp.Models.User;

namespace HotelBookingTest
{
    public class UserServiceTest : IDisposable
    {
        DbContextOptions options;
        HotelContext context;
        UserRepository repository;
        Moq.Mock<ILogger<UserRepository>> loggerUserRepo;
        Moq.Mock<ILogger<UserService>> loggerUserService;
        Moq.Mock<TokenService> mockTokenService;
        Moq.Mock<IConfiguration> mockConfiguration;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            loggerUserRepo = new Moq.Mock<ILogger<UserRepository>>();
            loggerUserService = new Moq.Mock<ILogger<UserService>>();
            repository = new UserRepository(context, loggerUserRepo.Object);
            mockConfiguration = new Moq.Mock<IConfiguration>();
            mockTokenService = new Moq.Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");
        }

        [Test]
        [TestCase("TestUser", "TestPassword", "TestHashKey", Roles.Admin)]
        public async Task TestAdd(string username, string password, string hashKey, Roles role)
        {
            var user = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = role
            };
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [TestCase("TestUser", "TestPassword", "TestHashKey")]
        public async Task TestAuthenticate(string username, string password, string hashKey)
        {
            var user = new UserCreateDTO
            {
                Username = "TestUser1",
                Password = "TestPassword1",
                Role = Roles.Admin
            };
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            var loggedInUser = await userService.Autheticate(new LoginRequestDTO
            {
                Username = user.Username,
                Password = user.Password
            });
            Assert.IsTrue(addedUser.Username == loggedInUser.Username);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
