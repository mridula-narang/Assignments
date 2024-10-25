using AutoMapper;
using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using InsuraceClaimApp.Repositories;
using InsuraceClaimApp.Services;
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
        ClaimRepository repository;
        Mock<ILogger<ClaimRepository>> logger;
        Mock<ILogger<ClaimRepository>> loggerClaimRepository;
        Mock<IMapper> mapper;
        Mock<IRepository<int, InsuranceClaim>> claimRepository;
        Mock<IRepository<int, Policy>> policyRepository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<ClaimRepository>>();
            loggerClaimRepository = new Mock<ILogger<ClaimRepository>>();
            repository = new ClaimRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            claimRepository = new Mock<IRepository<int, InsuranceClaim>>();
            policyRepository = new Mock<IRepository<int, Policy>>();
        }

        [Test]
        public async Task AddClaimTest()
        {
            //Arrange
            var claim = new InsuranceClaim
            {
                ClaimId = 1,
                PolicyNumber = 1, // Changed from PolicyId to PolicyNumber
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                DocumentPath = "path"
            };
            var claimRequest = new ClaimRequestDTO
            {
                PolicyId = 1,
                ClaimType = "Theft",
                IncidentDate = DateTime.Now,
                UserName = "user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Document = null
            };
            var policy = new Policy
            {
                PolicyId = 1,
                PolicyName = "Policy1",
                PolicyType = "Type1",
                UserName = "user"
            };
            var policyList = new List<Policy>
            {
                policy
            };
            var claimRequestList = new List<ClaimRequestDTO>
            {
                claimRequest
            };
            var policyDTO = new PolicyDTO
            {
                PolicyName = "Policy1",
                PolicyType = "Type1",
                UserName = "user"
            };
            var policyDTOList = new List<PolicyDTO>
            {
                policyDTO
            };
            mapper.Setup(m => m.Map<PolicyDTO>(policy)).Returns(policyDTO);
            mapper.Setup(m => m.Map<Policy>(policyDTO)).Returns(policy);
            mapper.Setup(m => m.Map<ClaimRequestDTO>(claim)).Returns(claimRequest);
            mapper.Setup(m => m.Map<IEnumerable<PolicyDTO>>(policyList)).Returns(policyDTOList);
            mapper.Setup(m => m.Map<IEnumerable<ClaimRequestDTO>>(claimRequestList)).Returns(claimRequestList);
            policyRepository.Setup(p => p.GetAll()).ReturnsAsync(policyList);
            claimRepository.Setup(c => c.Add(claim)).ReturnsAsync(claim);
            var service = new ClaimService(claimRepository.Object, policyRepository.Object, mapper.Object, new Mock<ILogger<ClaimService>>().Object);

            //Act
            var result = await service.AddClaimAsync(claimRequest);
            Assert.NotNull(result);
            Assert.AreEqual(claim.ClaimId, result.ClaimId);
            Assert.AreEqual(claim.PolicyNumber, result.PolicyNumber); // Changed from PolicyId to PolicyNumber
            Assert.AreEqual(claim.ClaimType, result.ClaimType);
            Assert.AreEqual(claim.IncidentDate, result.IncidentDate);
            Assert.AreEqual(claim.UserName, result.UserName);
            Assert.AreEqual(claim.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(claim.Email, result.Email);
            Assert.AreEqual(claim.DocumentPath, result.DocumentPath);
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
