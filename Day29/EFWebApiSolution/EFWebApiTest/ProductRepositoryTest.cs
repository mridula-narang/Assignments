using EFWebApiApp.Contexts;
using EFWebApiApp.Exceptions;
using EFWebApiApp.Models;
using EFWebApiApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWebApiTest
{
    public class ProductRepositoryTest : IDisposable
    {
        DbContextOptions options;
        ShoppingContext context;
        ProductRepository repository;
        Mock<ILogger<ProductRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductRepository>>();
            repository = new ProductRepository(context,logger.Object);
        }

        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        [TestCase("TestAdd2", 120, 4, "", "Test description for Product", 2)]
        public async Task TestAdd(string name, float price, int quantity, string image, string desc, int id)
        {
            //Arrange
            Product product = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                BasicImage = image,
                Description = desc
            };
            //Act
            var result = await repository.Add(product);
            //Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        public async Task TestAddException(string name, float price, int quantity, string image, string desc, int id)
        {
            //Arrange
            Product product = new Product
            {
                Name = null,
                Price = price,
                Quantity = quantity,
                BasicImage = image,
                Description = desc
            };

            //Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(product));
        }

        //write tests for Get() and GetAll() methods of ProductRepository
        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        [TestCase("TestAdd2", 150, 2, "", "Another test description for Product", 2)]
        public async Task Get(string name, float price, int quantity, string image, string desc, int id)
        {
            // Arrange
            var product = new Product { Name = name, Price = price, Quantity = quantity, BasicImage = image, Description = desc };
            await repository.Add(product);

            // Act
            var result = await repository.Get(id);

            // Assert
            Assert.AreEqual(name, result.Name);
        }
        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        [TestCase("TestAdd2", 150, 2, "", "Another test description for Product", 2)]
        public async Task GetAll(string name, float price, int quantity, string image, string desc, int id)
        {
            // Arrange
            var product = new Product { Name = name, Price = price, Quantity = quantity, BasicImage = image, Description = desc };
            await repository.Add(product);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.IsTrue(result.Any(p => p.Name == name && p.Price == price && p.Quantity == quantity && p.BasicImage == image && p.Description == desc));
        }


        //write tests for delete method of ProductRepository
        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        [TestCase("TestAdd2", 150, 2, "", "Another test description for Product", 2)]
        public async Task Delete(string name, float price, int quantity, string image, string desc, int id)
        {
            // Arrange
            var product = new Product { Name = name, Price = price, Quantity = quantity, BasicImage = image, Description = desc };
            await repository.Add(product);

            // Act
            var result = await repository.Delete(id);

            // Assert
            Assert.AreEqual(name, result.Name);
        }

        //write tests for update method of ProductRepository
        [Test]
        [TestCase("Original Name", 100, 5, "original.jpg", "Original Description", 1)]
        [TestCase("Second Product", 200, 10, "second.jpg", "Second Description", 2)]
        public async Task Update(string name, float price, int quantity, string image, string desc, int id)
        {
            // Arrange
            var originalProduct = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                BasicImage = image,
                Description = desc
            };

            // Add the original product to the repository
            await repository.Add(originalProduct);

            // New product details for updating
            var updatedProduct = new Product
            {
                Name = "Updated Name",
                Price = 150,
                Quantity = 15,
                BasicImage = "updated.jpg",
                Description = "Updated Description"
            };

            // Act
            var result = await repository.Update(id, updatedProduct);

            // Assert
            Assert.AreEqual("Updated Name", result.Name);
            Assert.AreEqual(150, result.Price);
            Assert.AreEqual(15, result.Quantity);
            Assert.AreEqual("updated.jpg", result.BasicImage);
            Assert.AreEqual("Updated Description", result.Description);
        }

        //write tests for exceptions in ProductRepository the not found exception, collection empty exception, and could not add exception. test passes if exception is thrown
        [Test]
        public async Task NotFoundException()
        {
            // Assert
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999)));
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(2)));
        }
        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        public async Task CollectionEmptyException(string name, float price, int quantity, string image, string desc, int id)
        {
            // Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }
        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        public async Task CouldNotAddException(string name, float price, int quantity, string image, string desc, int id)
        {
            // Arrange
            var product = new Product { Name = null, Price = price, Quantity = quantity, BasicImage = image, Description = desc };

            // Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(product));
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
