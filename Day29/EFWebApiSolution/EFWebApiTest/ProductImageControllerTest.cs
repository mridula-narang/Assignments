using AutoMapper;
using EFWebApiApp.Contexts;
using EFWebApiApp.Controllers;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Repositories;
using EFWebApiApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWebApiTest
{
    public class ProductImageControllerTest
    {
        DbContextOptions<ShoppingContext> options;
        ShoppingContext context;
        ProductImageRepository repository;
        Mock<ILogger<ProductImageRepository>> logger;
        private Mock<ILogger<ProductImageController>> loggerController;
        Mock<IMapper> mapper;
        IProductImageService productImageService;
        ProductImage productImage;
        Mock<IRepository<int, Product>> productRepository; // Added mock for product repository

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductImageRepository>>();
            loggerController = new Mock<ILogger<ProductImageController>>();
            repository = new ProductImageRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            productRepository = new Mock<IRepository<int, Product>>(); // Initialize the mock
            productImageService = new ProductImageService(repository, productRepository.Object); // Pass the mock to the service
        }

        [Test]
        public async Task AddProductImagesTest()
        {
            // Arrange
            var productImage = new ProductImage
            {
                ProductId = 1,
                ImageUrl = "http://test.com/image1.jpg"
            };
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            productRepository.Setup(p => p.Get(1)).ReturnsAsync(product); // Setup the mock to return the product

            // Act
            await productImageService.AddImagesToProduct(1, new List<string> { productImage.ImageUrl });

            // Assert
            var result = await repository.Get(1);
            Assert.AreEqual(productImage.ProductId, result.ProductId);
            Assert.AreEqual(productImage.ImageUrl, result.ImageUrl);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }

}
