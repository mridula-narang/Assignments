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
using System.Xml.Linq;

namespace InsuranceClaimTest
{
    public class PolicyRepositoryTest : IDisposable
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyRepository repository;
        Mock<ILogger<PolicyRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyRepository>>();
            repository = new PolicyRepository(context, logger.Object);
        }

        [Test]
        [TestCase(1, "Test Policy1", "Test Policy Type", "Test User1")]
        [TestCase(2, "Test Policy2", "Test Policy Type", "Test User2")]
        public async Task TestAdd(int policyId, string policyName, string policyType, string userName)
        {
            //Arrange
            Policy policy = new Policy
            {
                PolicyId = policyId,
                PolicyName = policyName,
                PolicyType = policyType,
                UserName = userName
            };

            //Act
            var result = await repository.Add(policy);

            //Assert
            Assert.AreEqual(policyId, result.PolicyId);
        }

        [Test]
        [TestCase(1, "Test Policy1", "Test Policy Type", "Test User1")]
        [TestCase(2, "Test Policy2", "Test Policy Type", "Test User2")]
        public async Task Get(int policyId, string policyName, string policyType, string userName)
        {
            //Arrange
            var policy = new Policy
            {
                PolicyId = policyId,
                PolicyName = policyName,
                PolicyType = policyType,
                UserName = userName
            };
            await repository.Add(policy);

            // Act
            var result = await repository.Get(policyId);

            // Assert
            Assert.AreEqual(policyName, result.PolicyName);
        }

        [Test]
        [TestCase(1, "Test Policy1", "Test Policy Type", "Test User1")]
        [TestCase(2, "Test Policy2", "Test Policy Type", "Test User2")]
        public async Task GetAll(int policyId, string policyName, string policyType, string userName)
        {
            //Arrange
            var policy = new Policy
            {
                PolicyId = policyId,
                PolicyName = policyName,
                PolicyType = policyType,
                UserName = userName
            };
            await repository.Add(policy);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Any(c => c.PolicyId == policyId && c.PolicyName == policyName && c.PolicyType == policyType && c.UserName == userName));
        }

        [Test]
        [TestCase(1, "Test Policy1", "Test Policy Type", "Test User1")]
        [TestCase(2, "Test Policy2", "Test Policy Type", "Test User2")]
        public async Task Delete(int policyId, string policyName, string policyType, string userName)
        {
            //Arrange
            var policy = new Policy
            {
                PolicyId = policyId,
                PolicyName = policyName,
                PolicyType = policyType,
                UserName = userName
            };
            await repository.Add(policy);

            // Act
            var result = await repository.Delete(policyId);

            // Assert
            Assert.AreEqual(policyId, result.PolicyId);
        }

        [Test]
        [TestCase(1, "Test Policy1", "Test Policy Type", "Test User1")]
        [TestCase(2, "Test Policy2", "Test Policy Type", "Test User2")]
        public async Task Update(int policyId, string policyName, string policyType, string userName)
        {
            //Arrange
            var policy = new Policy
            {
                PolicyId = policyId,
                PolicyName = policyName,
                PolicyType = policyType,
                UserName = userName
            };
            await repository.Add(policy);

            // Act
            policy.PolicyName = "Updated Policy Name";
            var result = await repository.Update(policyId, policy);

            // Assert
            Assert.AreEqual("Updated Policy Name", result.PolicyName);
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
