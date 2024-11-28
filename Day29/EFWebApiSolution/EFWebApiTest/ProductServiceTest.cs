using AutoMapper;
using EFWebApiApp.Contexts;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;
using EFWebApiApp.Repositories;
using EFWebApiApp.Services;
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
    public class ProductServiceTest
    {
        DbContextOptions options;
        ShoppingContext context;
        ProductRepository repository;
        Mock<ILogger<ProductRepository>> logger;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingList")
                .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductRepository>>();
            repository = new ProductRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task AddProductTest()
        {
            //arrange
            var product = new ProductDTO
            {
                Name = "Test Product",
                PricePerUnit = 100,
                Quantity = 10,
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 100,
                Quantity = 10,
            };
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            IProductService productService = new ProductService(repository, mapper.Object);
            //act
            var result = await productService.AddProduct(product);
            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetProductByIdTest()
        {
            //arrange
            var product = new Product
            {
                Name = "Test Product",
                Price = 100,
                Quantity = 10,
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 100,
                Quantity = 10,
            };
            context.Products.Add(product);
            context.SaveChanges();
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            IProductService productService = new ProductService(repository, mapper.Object);
            //act
            var result = await productService.GetProductById(1);
            //assert
            Assert.AreEqual(product.Name, result.Name);
        }

        [Test]
        public async Task GetProductsTest()
        {
            //arrange
            var products = new List<Product>
            {
                    new Product
                    {
                        Name = "Test Product 1",
                        Price = 100,
                        Quantity = 10,
                    },
                    new Product
                    {
                        Name = "Test Product 2",
                        Price = 200,
                        Quantity = 20,
                    }
            };
            context.Products.AddRange(products);  
            context.SaveChanges();

            var productDtos = new List<ProductDTO>
            {
                new ProductDTO { Name = "Test Product 1", PricePerUnit = 100, Quantity = 10 },
                new ProductDTO { Name = "Test Product 2", PricePerUnit = 200, Quantity = 20 }
            };

            mapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(It.IsAny<IEnumerable<Product>>()))
                  .Returns(productDtos);

            IProductService productService = new ProductService(repository, mapper.Object);

            //act
            var result = await productService.GetProducts();

            //assert
            Assert.AreEqual(productDtos.Count, result.Count());
        }

        [Test]
        public async Task UpdateProductPriceTest()
        {
            //arrange
            var product = new Product
            {
                Id = 1,  
                Name = "Test Product",
                Price = 100,
                Quantity = 10,
            };
            context.Products.Add(product);  
            context.SaveChanges();

            var productDto = new ProductDTO
            {
                Name = "Test Product",
                PricePerUnit = 200,
                Quantity = 10
            };

            mapper.Setup(m => m.Map<ProductDTO>(It.IsAny<Product>()))
                  .Returns(productDto);

            IProductService productService = new ProductService(repository, mapper.Object);

            //act
            var result = await productService.UpdateProductPrice(1, 200);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.PricePerUnit); 
        }
    }
}


