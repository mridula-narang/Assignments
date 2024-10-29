using AutoMapper;
using InsuraceClaimApp.Controllers;
using InsuraceClaimApp.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace InsuranceClaimTest
{
    public class ClaimControllerTest
    {
        private Mock<IClaimService> _claimServiceMock;
        private Mock<IMapper> _mapper;
        private ClaimController _controller;

        [SetUp]
        public void Setup()
        {
            _claimServiceMock = new Mock<IClaimService>();
            _mapper = new Mock<IMapper>();
            _controller = new ClaimController(_claimServiceMock.Object, _mapper.Object);
        }
        
        [Test]
        public async Task ReportTest()
        {
            // Arrange
            var claimRequest = new ClaimRequestDTO
            {
                PolicyId = 1,
                ClaimType = "Test type",
                IncidentDate = DateTime.Now,
                UserName = "Test user",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy content")), 0, 0, "Data", "dummy.txt")
            };

            var expectedClaim = new InsuranceClaim(); // Create an instance of InsuranceClaim that you expect to be returned

            _claimServiceMock.Setup(s => s.AddClaimAsync(claimRequest)).ReturnsAsync(expectedClaim);

            // Act
            var result = await _controller.ReportClaim(claimRequest);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedClaim = okResult.Value as InsuranceClaim;
            Assert.IsNotNull(returnedClaim);
            Assert.AreEqual(expectedClaim, returnedClaim); 
        }

        [Test]
        public async Task GetClaimTest()
        {
            // Arrange
            var claimId = 1;
            var expectedClaim = new InsuranceClaim(); // Create an instance of InsuranceClaim that you expect to be returned

            _claimServiceMock.Setup(s => s.GetClaimByIdAsync(claimId)).ReturnsAsync(expectedClaim);

            // Act
            var result = await _controller.GetClaim(claimId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedClaim = okResult.Value as InsuranceClaim;
            Assert.IsNotNull(returnedClaim);
            Assert.AreEqual(expectedClaim, returnedClaim);
        }

        [Test]
        public async Task GetAllClaimsTest()
        {
            // Arrange
            var expectedClaims = new List<InsuranceClaim> { new InsuranceClaim(), new InsuranceClaim() }; // Create a list of InsuranceClaim that you expect to be returned

            _claimServiceMock.Setup(s => s.GetAllClaimsAsync()).ReturnsAsync(expectedClaims);

            // Act
            var result = await _controller.GetAllClaims();

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedClaims = okResult.Value as IEnumerable<InsuranceClaim>;
            Assert.IsNotNull(returnedClaims);
            Assert.AreEqual(expectedClaims, returnedClaims);
        }

        [Test]
        public async Task DeleteClaimTest()
        {
            // Arrange
            var claimId = 1;

            // Act
            var result = await _controller.DeleteClaim(claimId);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);

            _claimServiceMock.Verify(s => s.DeleteClaimAsync(claimId), Times.Once);
        }

    }

}
