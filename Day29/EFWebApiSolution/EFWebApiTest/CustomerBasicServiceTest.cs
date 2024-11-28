using AutoMapper;
using EFWebApiApp.Contexts;
using EFWebApiApp.Exceptions;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;
using EFWebApiApp.Repositories;
using EFWebApiApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWebApiTest
{
    public class CustomerBasicServiceTest : IDisposable
    {
        DbContextOptions options;
        ShoppingContext context;
        Mock<CustomerRepository> repository;
        Mock<ILogger<CustomerRepository>> logger;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingList")
                .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<CustomerRepository>>();
            repository = new Mock<CustomerRepository>(context, logger.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task CreateCustomerTest()
        {
            //arrange
            var customer = new CustomerDTO
            {
                Name = "Test name 1",
                Email = "test1@gmail.com",
                Phone = "1234567890",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            var customerEntity = new Customer
            {
                Name = "Test name 2",
                Email = "test2@gmail.com",
                Phone = "7458459494",
                DateOfBirth = new DateTime(2000, 1, 1)
            };
            mapper.Setup(m => m.Map<Customer>(customer)).Returns(customerEntity);
            ICustomerBasicService customerBasic = new CustomerBasicService(repository.Object, mapper.Object);
            //act
            var result = await customerBasic.CreateCustomer(customer);
            //assert
            Assert.IsNotNull(result);

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
