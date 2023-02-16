
using API;
using API.Controllers;
using API.Models;
using API.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Test;

public class BoatControllerTests
{
    private readonly IUserService _fakeUserService;
    private readonly BoatAppContext _fakeDbContext;
    private readonly BoatController _boatController;

    public BoatControllerTests()
    {
        _fakeUserService = A.Fake<IUserService>();
        _fakeDbContext = A.Fake<BoatAppContext>();
        _boatController = new BoatController(_fakeDbContext, _fakeUserService);
    }

    [Fact]
    public async Task AddBoat_ReturnsBadRequest_WhenNameIsNullOrEmpty()
    {
        // Arrange
        var newBoat = new BoatDto { name = "", description = "Description", image_url = "ImageUrl" };

        // Act
        var result = await _boatController.AddBoat(newBoat);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = (BadRequestObjectResult)result.Result;
        Assert.Equal("Name is required.", badRequestResult.Value);
    }

    [Fact]
    public async Task AddBoat_ReturnsNotFound_WhenUserIsNotFound()
    {
        // Arrange
        A.CallTo(() => _fakeUserService.GetMyName()).Returns("fakeuser");
        A.CallTo(() => _fakeDbContext.Users).Returns(A.Fake<DbSet<User>>());

        var newBoat = new BoatDto { name = "BoatName", description = "Description", image_url = "ImageUrl" };

        // Act
        var result = await _boatController.AddBoat(newBoat);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
        var notFoundResult = (NotFoundObjectResult)result.Result;
        Assert.Equal("User not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task AddBoat_ReturnsOk_WhenBoatIsAddedSuccessfully()
    {
        // Arrange
        var user = new User { Username = "fakeuser", UserId = 1 };
        A.CallTo(() => _fakeUserService.GetMyName()).Returns(user.Username);
        A.CallTo(() => _fakeDbContext.SaveChanges()).Returns(1);

        var newBoat = new BoatDto { name = "BoatName", description = "Description", image_url = "ImageUrl" };

        // Act
        var result = await _boatController.AddBoat(newBoat);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        var userDto = (UserDto)okResult.Value;
        Assert.Equal(user.Username, userDto.username);
        Assert.Single(userDto.boats);
        var boat = userDto.boats.First();
        Assert.Equal(newBoat.name, boat.name);
        Assert.Equal(newBoat.description, boat.description);
        Assert.Equal(newBoat.image_url, boat.image_url);
    }
}