using InsuraceClaimApp.Controllers;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceClaimTest
{
    public class UserControllerTest
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<UserController>> _loggerMock;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_loggerMock.Object, _userServiceMock.Object);
        }

        [Test]
        public async Task RegisterTest()
        {
            // Arrange
            var createDTO = new UserCreateDTO
            {
                Username = "testuser",
                Password = "Password123",
            };
            var loginResponse = new LoginResponseDTO
            {
                Username = createDTO.Username,
                Token = "dummyToken"
            };
            _userServiceMock.Setup(s => s.Register(createDTO)).ReturnsAsync(loginResponse);

            // Act
            var result = await _controller.Register(createDTO);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(loginResponse, okResult.Value);
        }

        [Test]
        public async Task RegisterExceptionTest()
        {
            // Arrange
            var createDTO = new UserCreateDTO();
            var exceptionMessage = "User already exists";
            _userServiceMock.Setup(s => s.Register(createDTO)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Register(createDTO);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            var errorResponse = badRequestResult.Value as ErrorResponseDTO;
            Assert.AreEqual(exceptionMessage, errorResponse.ErrorMessage);
            Assert.AreEqual(500, errorResponse.ErrorNumber);
        }

        [Test]
        public async Task LoginTest()
        {
            // Arrange
            var loginDTO = new LoginRequestDTO
            {
                Username = "testuser",
                Password = "Password123"
            };
            var loginResponse = new LoginResponseDTO
            {
                Username = loginDTO.Username,
                Token = "dummyToken"
            };
            _userServiceMock.Setup(s => s.Autheticate(loginDTO)).ReturnsAsync(loginResponse);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(loginResponse, okResult.Value);
        }

        [Test]
        public async Task LoginInvalidCredentialsTest()
        {
            // Arrange
            var loginDTO = new LoginRequestDTO();
            var exceptionMessage = "Invalid credentials";
            _userServiceMock.Setup(s => s.Autheticate(loginDTO)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsNotNull(result);
            var unauthorizedResult = result.Result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
            var errorResponse = unauthorizedResult.Value as ErrorResponseDTO;
            Assert.AreEqual(401, errorResponse.ErrorNumber);
        }
    }
}
