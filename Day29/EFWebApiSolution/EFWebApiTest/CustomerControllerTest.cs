using AutoMapper;
using EFWebApiApp.Contexts;
using EFWebApiApp.Controllers;
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
    public class CustomerControllerTest : IDisposable
    {
        DbContextOptions options;
        ShoppingContext context;
        CustomerRepository repository;
        Mock<ILogger<CustomerRepository>> logger;
        private Mock<ILogger<CustomerController>> loggerController;
        Mock<IMapper> mapper;
        ICustomerBasicService customerBasicService;
        CustomerDTO customer;
        Customer customerEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<CustomerRepository>>();
            loggerController = new Mock<ILogger<CustomerController>>();
            repository = new CustomerRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            customerBasicService = new CustomerBasicService(repository, mapper.Object);
        }

        [Test]
        public async Task CreateCustomerTest()
        {
            //arrange
            var customer = new CustomerDTO
            {
                Name = "Test Customer1",
                Email = "test1@gmail.com",
                Phone = "1234567890",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            var customerEntity = new Customer
            {
                Name = "Test Customer2",
                Email = "test2@gmail.com",
                Phone = "865999955",
                DateOfBirth = new DateTime(1995, 10, 13)
            };
            mapper.Setup(m => m.Map<Customer>(customer)).Returns(customerEntity);
            var controller = new CustomerController(customerBasicService, loggerController.Object);

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
