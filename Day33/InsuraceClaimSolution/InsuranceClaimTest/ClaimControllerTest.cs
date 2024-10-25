//using InsuraceClaimApp.Controllers;
//using InsuraceClaimApp.Interfaces;
//using InsuraceClaimApp.Models.DTOs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace InsuranceClaimTest
//{
//    public class ClaimControllerTest
//    {
//        private Mock<IClaimService> _claimServiceMock;
//        private ClaimController _controller;

//        [SetUp]
//        public void Setup()
//        {
//            _claimServiceMock = new Mock<IClaimService>();
//            _controller = new ClaimController(_claimServiceMock.Object);
//        }

//        [Test]
//        public async Task ReportClaim_ValidClaim_ReturnsCreated()
//        {
//            // Arrange
//            var claimRequest = new ClaimRequestDTO
//            {
//                PolicyId = 1,
//                ClaimType = "Accident",
//                IncidentDate = new System.DateTime(2024, 10, 23),
//                UserName = "testuser",
//                PhoneNumber = "1234567890",
//                Email = "test@example.com",
//                Document = CreateMockFormFile("document.pdf", "application/pdf")
//            };

//            var createdClaim = new ClaimRequestDTO
//            {
//                ClaimId = 1,
//                PolicyId = 1,
//                ClaimType = "Accident",
//                IncidentDate = new System.DateTime(2024, 10, 23),
//                UserName = "testuser"
//            };

//            _claimServiceMock.Setup(s => s.AddClaimAsync(claimRequest)).ReturnsAsync(createdClaim);

//            // Act
//            var result = await _controller.ReportClaim(claimRequest);

//            // Assert
//            Assert.IsNotNull(result);
//            var createdAtActionResult = result as CreatedAtActionResult;
//            Assert.IsNotNull(createdAtActionResult);
//            Assert.AreEqual(201, createdAtActionResult.StatusCode);
//            Assert.AreEqual("GetClaim", createdAtActionResult.ActionName);
//            Assert.AreEqual(createdClaim.ClaimId, createdAtActionResult.RouteValues["id"]);
//            Assert.AreEqual(createdClaim, createdAtActionResult.Value);
//        }

//        [Test]
//        public async Task GetClaim_ValidId_ReturnsOk()
//        {
//            // Arrange
//            var claimId = 1;
//            var claim = new ClaimRequestDTO
//            {
//                ClaimId = claimId,
//                PolicyId = 1,
//                ClaimType = "Accident",
//                IncidentDate = new System.DateTime(2024, 10, 23),
//                UserName = "testuser"
//            };
//            _claimServiceMock.Setup(s => s.GetClaimByIdAsync(claimId)).ReturnsAsync(claim);

//            // Act
//            var result = await _controller.GetClaim(claimId);

//            // Assert
//            Assert.IsNotNull(result);
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            Assert.AreEqual(claim, okResult.Value);
//        }

//        [Test]
//        public async Task GetClaim_InvalidId_ReturnsNotFound()
//        {
//            // Arrange
//            var claimId = 999; // Non-existent ID
//            _claimServiceMock.Setup(s => s.GetClaimByIdAsync(claimId)).ReturnsAsync((ClaimDTO)null);

//            // Act
//            var result = await _controller.GetClaim(claimId);

//            // Assert
//            Assert.IsNotNull(result);
//            var notFoundResult = result as NotFoundResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//        }

//        [Test]
//        public async Task GetAllClaims_ReturnsOkWithClaims()
//        {
//            // Arrange
//            var claims = new List<ClaimRequestDTO>
//            {
//                new ClaimRequestDTO { ClaimId = 1, PolicyId = 1, ClaimType = "Accident", UserName = "user1" },
//                new ClaimRequestDTO { ClaimId = 2, PolicyId = 2, ClaimType = "Theft", UserName = "user2" }
//            };
//            _claimServiceMock.Setup(s => s.GetAllClaimsAsync()).ReturnsAsync(claims);

//            // Act
//            var result = await _controller.GetAllClaims();

//            // Assert
//            Assert.IsNotNull(result);
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            Assert.AreEqual(claims, okResult.Value);
//        }

//        [Test]
//        public async Task DeleteClaim_ValidId_ReturnsNoContent()
//        {
//            // Arrange
//            var claimId = 1;
//            _claimServiceMock.Setup(s => s.DeleteClaimAsync(claimId)).Returns(Task.CompletedTask);

//            // Act
//            var result = await _controller.DeleteClaim(claimId);

//            // Assert
//            Assert.IsNotNull(result);
//            var noContentResult = result as NoContentResult;
//            Assert.IsNotNull(noContentResult);
//            Assert.AreEqual(204, noContentResult.StatusCode);
//        }

//        [Test]
//        public async Task DeleteClaim_InvalidId_ReturnsNotFound()
//        {
//            // Arrange
//            var claimId = 999; // Non-existent ID
//            _claimServiceMock.Setup(s => s.DeleteClaimAsync(claimId)).ThrowsAsync(new KeyNotFoundException());

//            // Act
//            var result = await _controller.DeleteClaim(claimId);

//            // Assert
//            Assert.IsNotNull(result);
//            var notFoundResult = result as NotFoundResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//        }

//        private IFormFile CreateMockFormFile(string fileName, string contentType)
//        {
//            var content = "Sample file content";
//            var stream = new MemoryStream();
//            var writer = new StreamWriter(stream);
//            writer.Write(content);
//            writer.Flush();
//            stream.Position = 0;
//            return new FormFile(stream, 0, stream.Length, "file", fileName)
//            {
//                Headers = new HeaderDictionary(),
//                ContentType = contentType
//            };
//        }
//    }
//}
