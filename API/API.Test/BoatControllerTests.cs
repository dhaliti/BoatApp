
using API;
using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test;

public class BoatControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly BoatController _boatController;

    public BoatControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _boatController = new BoatController(null, _mockUserService.Object);
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
    
}
    
   