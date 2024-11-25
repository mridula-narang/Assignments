using AutoMapper;
using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using InsuraceClaimApp.Repositories;
using InsuraceClaimApp.Services;
using InsuraceClaimApp.Exceptions;
using Microsoft.AspNetCore.Http;
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
    public class ClaimServiceTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyRepository policyRepository;
        ClaimRepository claimRepository;
        Mock<ILogger<ClaimRepository>> loggerClaimRepo;
        Mock<ILogger<ClaimService>> loggerClaimService;
        Mock<ILogger<PolicyRepository>> loggerPolicyRepo;
        Mock<ILogger<PolicyService>> loggerPolicyService;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
           .UseInMemoryDatabase("TestAdd")
             .Options;
            context = new InsuranceContext(options);
            loggerPolicyRepo = new Mock<ILogger<PolicyRepository>>();
            loggerPolicyService = new Mock<ILogger<PolicyService>>();
            policyRepository = new PolicyRepository(context, loggerPolicyRepo.Object);
            loggerClaimRepo = new Mock<ILogger<ClaimRepository>>();
            loggerClaimService = new Mock<ILogger<ClaimService>>();
            claimRepository = new ClaimRepository(context, loggerClaimRepo.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task AddClaimTest()
        {
            // Arrange
            var policy = new Policy
            {
                PolicyId = 1,
                PolicyName = "Policy1",
                PolicyType = "Type1",
                UserName = "user"
            };

            // Add policy to the context
            await context.Policies.AddAsync(policy);
            await context.SaveChangesAsync();

            var claimRequest = new ClaimRequestDTO
            {
                PolicyId = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt")
            };

            var claim = new InsuranceClaim
            {
                ClaimId = 1,
                PolicyNumber = claimRequest.PolicyId,
                ClaimType = claimRequest.ClaimType,
                IncidentDate = claimRequest.IncidentDate,
                UserName = claimRequest.UserName,
                PhoneNumber = claimRequest.PhoneNumber,
                Email = claimRequest.Email,
                DocumentPath = "path"
            };

            mapper.Setup(m => m.Map<InsuranceClaim>(It.IsAny<ClaimRequestDTO>())).Returns(claim);

            var service = new ClaimService(claimRepository, policyRepository, mapper.Object, loggerClaimService.Object);

            // Act
            var result = await service.AddClaimAsync(claimRequest);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(claim.ClaimId, result.ClaimId);
            Assert.AreEqual(claim.PolicyNumber, result.PolicyNumber);
            Assert.AreEqual(claim.ClaimType, result.ClaimType);
            Assert.AreEqual(claim.IncidentDate, result.IncidentDate);
            Assert.AreEqual(claim.UserName, result.UserName);
            Assert.AreEqual(claim.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(claim.Email, result.Email);
            Assert.AreEqual("wwwroot\\documents\\dummy.txt", result.DocumentPath);
        }

        [Test]
        public async Task GetClaimByIdTest()
        {
            // Arrange
            var claim = new InsuranceClaim
            {
                ClaimId = 1,
                PolicyNumber = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                DocumentPath = "path"
            };

            // Add claim to the context
            await context.Claims.AddAsync(claim);
            await context.SaveChangesAsync();

            var service = new ClaimService(claimRepository, policyRepository, mapper.Object, loggerClaimService.Object);

            // Act
            var result = await service.GetClaimByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(claim.ClaimId, result.ClaimId);
        }

        [Test]
        public async Task GetAllClaimsTest()
        {
            // Arrange
            var claims = new List<InsuranceClaim>
            {
                new InsuranceClaim
                {
                    ClaimId = 1,
                    PolicyNumber = 1,
                    ClaimType = "Test claim type",
                    IncidentDate = DateTime.Now,
                    UserName = "user1",
                    PhoneNumber = "1234567890",
                    Email = "test1@gmail.com",
                    DocumentPath = "path1",
                },
                new InsuranceClaim
                {
                    ClaimId = 2,
                    PolicyNumber = 2,
                    ClaimType = "Test claim type",
                    IncidentDate = DateTime.Now,
                    UserName = "user2",
                    PhoneNumber = "1234567890",
                    Email = "test2@gmail.com",
                    DocumentPath = "path2",
                }
            };
            context.Claims.AddRange(claims);
            await context.SaveChangesAsync();

            var claimDtos = new List<ClaimRequestDTO>
            {
                new ClaimRequestDTO
                {
                    PolicyId = 1,
                    ClaimType = "Test claim type",
                    IncidentDate = DateTime.Now,
                    UserName = "user1",
                    PhoneNumber = "1234567890",
                    Email = "test1@gmail.com",
                    Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt") 
                },

                new ClaimRequestDTO
                {
                    PolicyId = 1,
                    ClaimType = "Test claim type",
                    IncidentDate = DateTime.Now,
                    UserName = "user2",
                    PhoneNumber = "1234567890",
                    Email = "test2@gmail.com",
                    Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt")
                }

            };

            mapper.Setup(m => m.Map<IEnumerable<ClaimRequestDTO>>(It.IsAny<IEnumerable<InsuranceClaim>>())).Returns(claimDtos);
            IClaimService claimService = new ClaimService(claimRepository, policyRepository, mapper.Object, loggerClaimService.Object);

            // Act
            var result = await claimService.GetAllClaimsAsync();

            // Assert
            Assert.AreEqual(claimDtos.Count,result.Count());
        }

        [Test]
        public async Task UpdateClaimTest()
        {
            // Arrange
            var claim = new InsuranceClaim
            {
                ClaimId = 1,
                PolicyNumber = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                DocumentPath = "path"
            };
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<InsuranceClaim>(It.IsAny<ClaimRequestDTO>())).Returns(claim);

            // Add claim to the context
            await context.Claims.AddAsync(claim);
            await context.SaveChangesAsync();

            var claimRequest = new ClaimRequestDTO
            {
                PolicyId = 1,
                ClaimType = "Medical",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt")
            };

            var service = new ClaimService(claimRepository, policyRepository, mapper.Object, loggerClaimService.Object);

            // Act
            var result = await service.UpdateClaimAsync(1, claimRequest);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(claim.ClaimId, result.ClaimId);
            Assert.AreEqual(claim.PolicyNumber, result.PolicyNumber);
            Assert.AreEqual(claim.ClaimType, result.ClaimType);
            Assert.AreEqual(claim.IncidentDate, result.IncidentDate);
            Assert.AreEqual(claim.UserName, result.UserName);
            Assert.AreEqual(claim.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(claim.Email, result.Email);
            Assert.AreEqual("wwwroot\\documents\\dummy.txt", result.DocumentPath);
        }

        [Test]
        public async Task DeleteClaimTest()
        {
            // Arrange
            var claim = new InsuranceClaim
            {
                ClaimId = 1,
                PolicyNumber = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                DocumentPath = "path"
            };

            // Add claim to the context
            await context.Claims.AddAsync(claim);
            await context.SaveChangesAsync();

            var service = new ClaimService(claimRepository, policyRepository, mapper.Object, loggerClaimService.Object);

            // Act
            await service.DeleteClaimAsync(1);

            // Assert
            var deletedClaim = await context.Claims.FindAsync(1);
            Assert.Null(deletedClaim);
        }

        [Test]

        //test for SaveDocumentAsync method
        public async Task SaveDocumentAsyncTest()
        {
            // Arrange
            var claimRequest = new ClaimRequestDTO
            {
                PolicyId = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt")
            };
            var policyRequest = new ClaimRequestDTO
            {
                PolicyId = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt")
            };

            var service = new ClaimService(claimRepository, policyRepository, mapper.Object, loggerClaimService.Object);

            // Act
            var result = await service.SaveDocumentAsync(claimRequest.Document);
            Assert.NotNull(result);
            Assert.AreEqual("wwwroot\\documents\\dummy.txt", result);
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
