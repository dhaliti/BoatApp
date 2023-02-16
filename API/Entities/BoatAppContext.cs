using API.Controllers;
using API.Models;
using API.Services;
using API;
using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mock;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace API.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<BoatAppContext> _contextMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _userServiceMock = new Mock<IUserService>();
            _contextMock = new Mock<BoatAppContext>();
            _controller = new AuthController(_configurationMock.Object, _userServiceMock.Object, _contextMock.Object);
        }

        [Fact]
        public void GetBoats_ReturnsExpectedResult()
        {
            // Arrange
            var username = "testuser";
            var user = new User { Username = username, UserId = 1 };
            var boats = new List<Boat> { new Boat { BoatId = 1, userId = user.UserId }, new Boat { BoatId = 2, userId = user.UserId } };
            _userServiceMock.Setup(x => x.GetMyName()).Returns(username);
            _contextMock.Setup(x => x.Users.FirstOrDefault(u => u.Username == username)).Returns(user);
            _contextMock.Setup(x => x.Boats.Where(b => b.userId == user.UserId)).Returns(boats.AsQueryable());

            // Act
            var result = _controller.GetBoats();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userDto = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(username, userDto.username);
            Assert.Equal(boats.Count, userDto.boats.Count);
        }
    }
}