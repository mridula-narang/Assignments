using AutoMapper;
using EFWebApiApp.Contexts;
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
    public class ProductImageServiceTest : IDisposable
    {
        DbContextOptions options;
        ShoppingContext context;
        ProductImageRepository repository;
        Mock<ILogger<ProductImageRepository>> logger;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingList")
                .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductImageRepository>>();
            repository = new ProductImageRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task AddProductImageTest()
        {
            //arrange
            var productImage = new ProductImage
            {
                ImageUrl = "Test Image.jpg",
                ProductId = 1,
            };

            //act
            await repository.Add(productImage);

            //assert
            var result = await context.ProductImages.FirstOrDefaultAsync();
            Assert.AreEqual(productImage.ImageUrl, result.ImageUrl);
            Assert.AreEqual(productImage.ProductId, result.ProductId);
        }

        [Test]
        public async Task GetImagesByProductId()
        {
            var productImage = new ProductImage
            {
                ImageUrl = "Test Image.jpg",
                ProductId = 1,
            };
            var prodImage = new ProductImage
            {
                ImageUrl = "Test Image.jpg",
                ProductId = 2,
            };
            await repository.Add(productImage);
            context.SaveChanges();
            await context.Products.FirstOrDefaultAsync();
            var result = await repository.Get(productImage.ImageId);
            Assert.AreEqual(productImage.ImageUrl, result.ImageUrl);
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
