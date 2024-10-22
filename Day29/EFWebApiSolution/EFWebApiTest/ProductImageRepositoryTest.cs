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
    public class ProductImageRepositoryTest : IDisposable
    {
        private DbContextOptions<ShoppingContext> _options;
        private ShoppingContext _context;
        private ProductImageRepository _repository;
        private Mock<ILogger<ProductImageRepository>> _logger;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestProductImageRepo")
              .Options;
            _context = new ShoppingContext(_options);
            _logger = new Mock<ILogger<ProductImageRepository>>();
            _repository = new ProductImageRepository(_context, _logger.Object);
        }

        [Test]
        [TestCase("https://example.com/image1.jpg", 1)]
        [TestCase("https://example.com/image2.jpg", 2)]
        public async Task TestAdd(string imageUrl, int productId)
        {
            // Arrange
            ProductImage productImage = new ProductImage
            {
                ImageUrl = imageUrl,
                ProductId = productId
            };

            // Act
            var result = await _repository.Add(productImage);

            // Assert
            Assert.AreEqual(imageUrl, result.ImageUrl);
            Assert.AreEqual(productId, result.ProductId);
        }

        [Test]
        public async Task TestAddException()
        {
            // Arrange
            ProductImage productImage = new ProductImage
            {
                ImageUrl = null, // Invalid URL
                ProductId = 1
            };

            // Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(productImage));
        }

        [Test]
        [TestCase(1)]
        public async Task TestGet(int imageId)
        {
            // Arrange
            var productImage = new ProductImage
            {
                ImageId = imageId,
                ImageUrl = "https://example.com/image1.jpg",
                ProductId = 1
            };
            await _repository.Add(productImage);

            // Act
            var result = await _repository.Get(imageId);

            // Assert
            Assert.AreEqual(imageId, result.ImageId);
        }

        [Test]
        public async Task TestGetException()
        {
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task TestGetAll()
        {
            // Arrange
            var productImage1 = new ProductImage
            {
                ImageUrl = "https://example.com/image1.jpg",
                ProductId = 1
            };
            var productImage2 = new ProductImage
            {
                ImageUrl = "https://example.com/image2.jpg",
                ProductId = 2
            };
            await _repository.Add(productImage1);
            await _repository.Add(productImage2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task TestGetAllException()
        {
            // Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
        }

        [Test]
        [TestCase(1)]
        public async Task TestDelete(int imageId)
        {
            // Arrange
            var productImage = new ProductImage
            {
                ImageId = imageId,
                ImageUrl = "https://example.com/image1.jpg",
                ProductId = 1
            };
            await _repository.Add(productImage);

            // Act
            var result = await _repository.Delete(imageId);

            // Assert
            Assert.AreEqual(imageId, result.ImageId);
        }

        [Test]
        public async Task TestDeleteException()
        {
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(999));
        }

        [Test]
        [TestCase(1)]
        public async Task TestUpdate(int imageId)
        {
            // Arrange
            var productImage = new ProductImage
            {
                ImageId = imageId,
                ImageUrl = "https://example.com/image1.jpg",
                ProductId = 1
            };
            await _repository.Add(productImage);

            var updatedProductImage = new ProductImage
            {
                ImageUrl = "https://example.com/updated-image.jpg",
                ProductId = 1
            };

            // Act
            var result = await _repository.Update(imageId, updatedProductImage);

            // Assert
            Assert.AreEqual("https://example.com/updated-image.jpg", result.ImageUrl);
        }

        [Test]
        public async Task TestUpdateException()
        {
            // Arrange
            var updatedProductImage = new ProductImage
            {
                ImageUrl = "https://example.com/updated-image.jpg",
                ProductId = 1
            };

            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(999, updatedProductImage));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
