using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;

namespace InsuranceClaimTest
{
    public class UserRepositoryTest : IDisposable
    {
        DbContextOptions options;
        InsuranceContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<UserRepository>>();
            repository = new UserRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            User user = new User
            {
                Username = "Test User1",
                Password = Encoding.UTF8.GetBytes("Test Password1"),
                HashKey = Encoding.UTF8.GetBytes("Test Hash"),
            };
            var addedUser = await repository.Add(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [Test]
        public async Task TestGet()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.Get(user.Username);
            Assert.IsNotNull(getUser);
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