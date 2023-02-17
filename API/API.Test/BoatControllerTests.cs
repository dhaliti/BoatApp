
using API;
using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test;

/*
 I didn't have much time left for unit testing and I encountered an issue with the DBContext.
 Neither FakeItEasy nor Moq seemed to mock it properly. 
 In my opinion, it might be better for unit-testing purposes to instantiate the DbContext directly in the controller 
 by using something like 'using (var conn = new NpgsqlConnection(connectionString)) {}
 */

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
    public async void AddBoat_ReturnsBadRequest_WhenNameIsNullOrEmpty()
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
    
   