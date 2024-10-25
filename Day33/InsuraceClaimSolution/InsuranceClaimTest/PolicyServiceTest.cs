using AutoMapper;
using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using InsuraceClaimApp.Repositories;
using InsuraceClaimApp.Services;
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
    public class PolicyServiceTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyRepository repository;
        Mock<ILogger<PolicyRepository>> loggerPolicyRepo;
        Mock<ILogger<PolicyRepository>> loggerPolicyService;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            loggerPolicyRepo = new Mock<ILogger<PolicyRepository>>();
            loggerPolicyService = new Mock<ILogger<PolicyRepository>>();
            repository = new PolicyRepository(context, loggerPolicyRepo.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task TestAdd()
        {
            var policy = new PolicyDTO
            {
                UserName = "Test user",
                PolicyName = "Test policy",
                PolicyType = "Test type"
            };
            var policyEntity = new Policy
            {
                UserName = "Test user",
                PolicyName = "Test policy",
                PolicyType = "Test type"
            };
            mapper.Setup(mbox => mbox.Map<Policy>(policy)).Returns(policyEntity);
            IPolicyService policyService = new PolicyService(repository, mapper.Object);
        }

        [Test]
        public async Task TestGetAllPoliciesAsync()
        {
            var policy = new PolicyDTO
            {
                UserName = "Test user",
                PolicyName = "Test policy",
                PolicyType = "Test type"
            };
            var policyEntity = new Policy
            {
                UserName = "Test user",
                PolicyName = "Test policy",
                PolicyType = "Test type"
            };
            mapper.Setup(mbox => mbox.Map<Policy>(policy)).Returns(policyEntity);
            IPolicyService policyService = new PolicyService(repository, mapper.Object);
            var addedPolicy = await policyService.AddPolicyAsync(policyEntity);
            var policies = await policyService.GetAllPoliciesAsync();
            Assert.IsTrue(policies.Count() == 1);
        }

        [Test]
        public async Task TestGetPolicyByIdAsync()
        {
            var policy = new PolicyDTO
            {
                UserName = "Test user",
                PolicyName = "Test policy",
                PolicyType = "Test type"
            };
            var policyEntity = new Policy
            {
                UserName = "Test user",
                PolicyName = "Test policy",
                PolicyType = "Test type"
            };
            mapper.Setup(mbox => mbox.Map<Policy>(policy)).Returns(policyEntity);
            IPolicyService policyService = new PolicyService(repository, mapper.Object);
            var addedPolicy = await policyService.AddPolicyAsync(policyEntity);
            var policies = await policyService.GetPolicyByIdAsync(1);
            Assert.IsTrue(policies.PolicyName == "Test policy");
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
