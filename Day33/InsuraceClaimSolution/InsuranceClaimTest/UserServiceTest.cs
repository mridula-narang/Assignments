using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Models.DTOs;
using InsuraceClaimApp.Repositories;
using InsuraceClaimApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimTest
{
    public class UserServiceTest : IDisposable
    {
        DbContextOptions options;
        InsuranceContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> loggerUserRepo;
        Mock<ILogger<UserService>> loggerUserService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>();
            repository = new UserRepository(context, loggerUserRepo.Object);
        }

        [Test]
        [TestCase("TestUser", "TestPassword", "TestHashKey")]
        public async Task TestAdd(string username, string password, string hashKey)
        {
            var user = new UserCreateDTO
            {
                Username = username,
                Password = password,
            };
            var userService = new UserService(repository, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [TestCase("TestUser", "TestPassword", "TestHashKey")]
        public async Task TestAuthenticate(string username, string password, string hashKey)
        {
            var user = new UserCreateDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
            };
            var userService = new UserService(repository, loggerUserService.Object);
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
            context?.Dispose();
        }
    }
}
