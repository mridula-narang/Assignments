using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static HotelBookingApp.Models.User;
using System.Text;
using HotelBookingApp.Contexts;
using Moq;
using HotelBookingApp.Exceptions;


namespace HotelBookingTest
{
    public class UserRepositoryTest
    {
        DbContextOptions options;
        HotelContext context;
        UserRepository repository;
        Moq.Mock<ILogger<UserRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<HotelContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new HotelContext(options);
            logger = new Moq.Mock<ILogger<UserRepository>>();
            repository = new UserRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
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
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.Get(addedUser.UserId);
            Assert.IsNotNull(getUser);
        }

        [Test]
        public async Task TestDelete()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            var deletedUser = await repository.Delete(addedUser.UserId);
            Assert.IsNotNull(deletedUser);
        }

        //test for get all method
        [Test]
        public async Task TestGetAll()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            var users = await repository.GetAll();
            Assert.IsTrue(users.Count() > 0);
        }

        //test for update method
        [Test]
        public async Task TestUpdate()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            addedUser.Username = "UpdatedUser";
            var updatedUser = await repository.Update(addedUser.UserId, addedUser);
            Assert.IsTrue(updatedUser.Username == addedUser.Username);
        }

        [Test]
        public async Task TestGetNotFound()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.Get(addedUser.UserId + 1);
            Assert.IsNull(getUser);
        }

        [Test]
        public async Task TestDeleteNotFound()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(addedUser.UserId + 1));
        }

        //could not add exception test for add method
        [Test]
        public async Task TestAddCouldNotAddException()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Users.Add(user);
            context.SaveChanges();
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(user));
        }

        

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}