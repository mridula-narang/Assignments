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
    public class CustomerRepositoryTest
    {
        DbContextOptions<ShoppingContext> options;
        ShoppingContext context;
        CustomerRepository repository;
        Mock<ILogger<CustomerRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase("TestCustomerDB")
                .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<CustomerRepository>>();
            repository = new CustomerRepository(context,logger.Object);
        }

        [Test]
        [TestCase("Harry", "harry@example.com", "1234567890", 30, "1994-06-15")]
        [TestCase("Hermione", "hermione@example.com", "0987654321", 25, "1999-05-10")]
        public async Task TestAddCustomer(string name, string email, string phone, int age, string dob)
        {
            // Arrange
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone,
                Age = age,
                DateOfBirth = DateTime.Parse(dob)
            };

            // Act
            var result = await repository.Add(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public async Task TestAddCustomer_Exception()
        {
            // Arrange
            var customer = new Customer
            {
                Name = null,  // Invalid data
                Email = "harry@example.com",
                Phone = "1234567890",
                Age = 30,
                DateOfBirth = DateTime.Parse("1994-06-15")
            };

            // Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(customer));
        }

        [Test]
        [TestCase(1, "Harry", "harry@example.com", "1234567890", 30, "1994-06-15")]
        public async Task TestGetCustomer(int id, string name, string email, string phone, int age, string dob)
        {
            // Arrange
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone,
                Age = age,
                DateOfBirth = DateTime.Parse(dob)
            };
            await repository.Add(customer);

            // Act
            var result = await repository.Get(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
        }

        public async Task NotFoundException()
        {
            // Assert
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999)));
            await Task.Run(() => Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(2)));
        }

        [Test]
        public async Task TestGetAllCustomers()
        {
            // Arrange
            var customer1 = new Customer
            {
                Name = "Harry",
                Email = "harry@example.com",
                Phone = "1234567890",
                Age = 30,
                DateOfBirth = DateTime.Parse("1994-06-15")
            };
            var customer2 = new Customer
            {
                Name = "Hermione",
                Email = "hermione@example.com",
                Phone = "0987654321",
                Age = 25,
                DateOfBirth = DateTime.Parse("1999-05-10")
            };
            await repository.Add(customer1);
            await repository.Add(customer2);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task TestGetAllCustomers_CollectionEmpty()
        {
            // Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        [TestCase(1, "John Doe", "john@example.com", "1234567890", 30, "1994-06-15")]
        public async Task TestDeleteCustomer(int id, string name, string email, string phone, int age, string dob)
        {
            // Arrange
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone,
                Age = age,
                DateOfBirth = DateTime.Parse(dob)
            };
            await repository.Add(customer);

            // Act
            var result = await repository.Delete(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public async Task TestDeleteCustomer_NotFound()
        {
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(999));
        }

        [Test]
        [TestCase(1, "John Doe", "john@example.com", "1234567890", 30, "1994-06-15", "Updated Name", "updated@example.com", "9876543210", 40)]
        public async Task TestUpdateCustomer(int id, string name, string email, string phone, int age, string dob, string updatedName, string updatedEmail, string updatedPhone, int updatedAge)
        {
            // Arrange
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone,
                Age = age,
                DateOfBirth = DateTime.Parse(dob)
            };
            await repository.Add(customer);

            var updatedCustomer = new Customer
            {
                Name = updatedName,
                Email = updatedEmail,
                Phone = updatedPhone,
                Age = updatedAge,
                DateOfBirth = DateTime.Parse(dob) // Keep DOB same
            };

            // Act
            var result = await repository.Update(id, updatedCustomer);

            // Assert
            Assert.AreEqual(updatedName, result.Name);
            Assert.AreEqual(updatedEmail, result.Email);
        }

        [Test]
        public async Task TestUpdateCustomer_NotFound()
        {
            var customer = new Customer
            {
                Name = "Updated Name",
                Email = "updated@example.com",
                Phone = "9876543210",
                Age = 40,
                DateOfBirth = DateTime.Parse("1980-01-01")
            };

            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(999, customer));
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
