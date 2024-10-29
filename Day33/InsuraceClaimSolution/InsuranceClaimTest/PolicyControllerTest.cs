using AutoMapper;
using InsuraceClaimApp.Controllers;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models.DTOs;
using InsuraceClaimApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimTest
{
    public class PolicyControllerTest
    {
        private Mock<IPolicyService> _policyServiceMock;
        private Mock<IMapper> _mapper;
        private PolicyController _controller;

        [SetUp]
        public void Setup()
        {
            _policyServiceMock = new Mock<IPolicyService>();
            _mapper = new Mock<IMapper>();
            _controller = new PolicyController(_policyServiceMock.Object, _mapper.Object);
        }

        [Test]
        public async Task GetPoliciesForUserTest()
        {
            // Arrange
            var username = "testuser";
            var policies = new List<Policy>
            {
                new Policy { PolicyId = 1, PolicyName = "Health Insurance", UserName = username },
                new Policy { PolicyId = 2, PolicyName = "Car Insurance", UserName = username }
            };
            _policyServiceMock.Setup(s => s.GetAllPoliciesAsync()).ReturnsAsync(policies);

            // Act
            var result = await _controller.GetPoliciesForUser(username);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var returnedPolicies = okResult.Value as List<Policy>;
            Assert.IsNotNull(returnedPolicies);
            Assert.AreEqual(2, returnedPolicies.Count);
        }

        [Test]
        public async Task GetPoliciesForUser_InvalidUserTest()
        {
            // Arrange
            var username = "nonexistentuser";
            var policies = new List<Policy>
            {
                new Policy { PolicyId = 1, PolicyName = "Health Insurance", UserName = "testuser" }
            };
            _policyServiceMock.Setup(s => s.GetAllPoliciesAsync()).ReturnsAsync(policies);

            // Act
            var result = await _controller.GetPoliciesForUser(username);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No policies found for this user.", notFoundResult.Value);
        }

        [Test]
        public async Task AddPolicyTest()
        {
            // Arrange
            var newPolicyDto = new PolicyDTO
            {
                PolicyName = "Life Insurance",
                PolicyType = "Term",
                UserName = "testuser"
            };
            var newPolicy = new Policy
            {
                PolicyId = 1,
                PolicyName = "Life Insurance",
                PolicyType = "Term",
                UserName = "testuser"
            };
            _mapper.Setup(m => m.Map<Policy>(newPolicyDto)).Returns(newPolicy);
            _policyServiceMock.Setup(s => s.AddPolicyAsync(newPolicy)).ReturnsAsync(newPolicy);

            // Act
            var result = await _controller.AddPolicy(newPolicyDto);

            // Assert
            Assert.IsNotNull(result);
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual("GetPolicyById", createdResult.ActionName);
            Assert.AreEqual(newPolicy.PolicyId, createdResult.RouteValues["id"]);
        }

        [Test]
        public async Task AddPolicy_InvalidPolicyTest()
        {
            // Arrange
            var invalidPolicyDto = new PolicyDTO(); // Missing required fields

            // Act
            var result = await _controller.AddPolicy(invalidPolicyDto);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Invalid policy details.", badRequestResult.Value);
        }

        [Test]
        public async Task GetPolicyByIdTest()
        {
            // Arrange
            var policyId = 1;
            var policy = new Policy
            {
                PolicyId = policyId,
                PolicyName = "Life Insurance",
                PolicyType = "Term",
                UserName = "testuser"
            };
            _policyServiceMock.Setup(s => s.GetPolicyByIdAsync(policyId)).ReturnsAsync(policy);

            // Act
            var result = await _controller.GetPolicyById(policyId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(policy, okResult.Value);
        }

        [Test]
        public async Task GetPolicyById_InvalidIdTest()
        {
            // Arrange
            var policyId = 999; // Nonexistent ID
            _policyServiceMock.Setup(s => s.GetPolicyByIdAsync(policyId)).ReturnsAsync((Policy)null);

            // Act
            var result = await _controller.GetPolicyById(policyId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
