using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimTest
{
    public class ClaimRepositoryTest : IDisposable
    {
        DbContextOptions<InsuranceContext> options;
        InsuranceContext context;
        ClaimRepository repository;
        Mock<ILogger<ClaimRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<ClaimRepository>>();
            repository = new ClaimRepository(context, logger.Object);
        }

        [Test]
        [TestCase(1, 1, "Test Claim Type", "2024-10-20", "Test user", "76894875894", "test@gmail.com", "test path")]
        [TestCase(2, 2, "Test Claim Type", "2024-10-12", "Test user", "885747583", "test@gmail.com", "test path")]

        public async Task TestAdd(int claimId, int policyNumber,string claimType, DateTime incidentDate, string userName, string phoneNumber, string email, string path)
        {
            //Arrange
            InsuranceClaim claim = new InsuranceClaim
            {
                ClaimId = claimId,
                PolicyNumber = policyNumber,
                ClaimType = claimType,
                IncidentDate = incidentDate,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                DocumentPath = path
            };

            //Act
            var result = await repository.Add(claim);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(claimId, result.ClaimId);
            Assert.AreEqual(policyNumber, result.PolicyNumber);
            Assert.AreEqual(claimType, result.ClaimType);
            Assert.AreEqual(incidentDate, result.IncidentDate);
            Assert.AreEqual(userName, result.UserName);
            Assert.AreEqual(phoneNumber, result.PhoneNumber);
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(path, result.DocumentPath);
        }

        [Test]
        [TestCase(1, 1, "Test Claim Type", "2024-10-20", "Test user", "76894875894", "test@gmail.com", "test path")]
        [TestCase(2, 2, "Test Claim Type", "2024-10-12", "Test user", "885747583", "test@gmail.com", "test path")]
        public async Task TestGet(int claimId, int policyNumber, string claimType, DateTime incidentDate, string userName, string phoneNumber, string email, string path)
        {
            //arrange
            var claim = new InsuranceClaim
            {
                ClaimId = claimId,
                PolicyNumber = policyNumber,
                ClaimType = claimType,
                IncidentDate = incidentDate,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                DocumentPath = path
            };
            await repository.Add(claim);

            //act
            var result = await repository.Get(claimId);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(claimId, result.ClaimId);
            Assert.AreEqual(policyNumber, result.PolicyNumber);
            Assert.AreEqual(claimType, result.ClaimType);
            Assert.AreEqual(incidentDate, result.IncidentDate);
            Assert.AreEqual(userName, result.UserName);
            Assert.AreEqual(phoneNumber, result.PhoneNumber);
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(path, result.DocumentPath);
        }

        [Test]
        [TestCase(1, 1, "Test Claim Type", "2024-10-20", "Test user", "76894875894", "test@gmail.com", "test path")]
        [TestCase(2, 2, "Test Claim Type", "2024-10-12", "Test user", "885747583", "test@gmail.com", "test path")]
        public async Task TestDelete(int claimId, int policyNumber, string claimType, DateTime incidentDate, string userName, string phoneNumber, string email, string path)
        {
            //arrange
            var claim = new InsuranceClaim
            {
                ClaimId = claimId,
                PolicyNumber = policyNumber,
                ClaimType = claimType,
                IncidentDate = incidentDate,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                DocumentPath = path
            };
            await repository.Add(claim);

            //act
            var result = await repository.Delete(claimId);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(claimId, result.ClaimId);
            Assert.AreEqual(policyNumber, result.PolicyNumber);
            Assert.AreEqual(claimType, result.ClaimType);
            Assert.AreEqual(incidentDate, result.IncidentDate);
            Assert.AreEqual(userName, result.UserName);
            Assert.AreEqual(phoneNumber, result.PhoneNumber);
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(path, result.DocumentPath);
        }

        [Test]
        [TestCase(1, 1, "Test Claim Type", "2024-10-20", "Test user", "76894875894", "test@gmail.com", "test path")]
        [TestCase(2, 2, "Test Claim Type", "2024-10-12", "Test user", "885747583", "test@gmail.com", "test path")]
        //test for get all method
        public async Task TestGetAll(int claimId, int policyNumber, string claimType, DateTime incidentDate, string userName, string phoneNumber, string email, string path)
        {
            //arrange
            var claim = new InsuranceClaim
            {
                ClaimId = claimId,
                PolicyNumber = policyNumber,
                ClaimType = claimType,
                IncidentDate = incidentDate,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                DocumentPath = path
            };
            await repository.Add(claim);
            //act
            var result = await repository.GetAll();
            //assert
            Assert.IsTrue(result.Any(c=>c.ClaimId == claimId && c.PolicyNumber == policyNumber && c.ClaimType == claimType && c.IncidentDate == incidentDate && c.UserName == userName && c.PhoneNumber == phoneNumber && c.Email == email && c.DocumentPath == path));
        }

        [Test]
        [TestCase(1, 1, "Test Claim Type", "2024-10-20", "Test user", "76894875894", "test@gmail.com", "test path")]
        [TestCase(2, 2, "Test Claim Type", "2024-10-12", "Test user", "885747583", "test@gmail.com", "test path")]
        //test for delete method
        public async Task TestDeleteException(int claimId, int policyNumber, string claimType, DateTime incidentDate, string userName, string phoneNumber, string email, string path)
        {
            //arrange
            var claim = new InsuranceClaim
            {
                ClaimId = claimId,
                PolicyNumber = policyNumber,
                ClaimType = claimType,
                IncidentDate = incidentDate,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                DocumentPath = path
            };
            await repository.Add(claim);
            //act
            var result = await repository.Delete(claimId);
            //assert
            Assert.AreEqual(claimId, result.ClaimId);
        }
        [Test]
        [TestCase(1, 1, "Test Claim Type", "2024-10-20", "Test user", "76894875894", "test@gmail.com", "test path")]
        [TestCase(2, 2, "Test Claim Type", "2024-10-12", "Test user", "885747583", "test@gmail.com", "test path")]
        //test for update method
        public async Task TestUpdate(int claimId, int policyNumber, string claimType, DateTime incidentDate, string userName, string phoneNumber, string email, string path)
        {
            //arrange
            var claim = new InsuranceClaim
            {
                ClaimId = claimId,
                PolicyNumber = policyNumber,
                ClaimType = claimType,
                IncidentDate = incidentDate,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                DocumentPath = path
            };
            await repository.Add(claim);
            //act
            claim.ClaimType = "Updated Claim Type";
            var result = await repository.Update(claimId, claim);
            //assert
            Assert.AreEqual("Updated Claim Type", result.ClaimType);
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
