using System.Linq.Expressions;
using API;
using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class AuthControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly BoatController _boatController;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _boatController = new BoatController(null, _mockUserService.Object);
        _authController = new AuthController(null, _mockUserService.Object, null);
    }
    
    [Fact]
    public void  Register_Returns_BadRequest_When_Username_Is_Empty()
    {
        // Arrange
        var request = new CredentialsDto() { Username = "", Password = "123"};
    
        // Act
        var result =  _authController.Register(request);
    
        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = (BadRequestObjectResult)result.Result;
        Assert.Equal("Username and password are required.", badRequestResult.Value);
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public  void Register_Returns_BadRequest_When_Password_Is_Empty()
    {
        // Arrange
        var request = new CredentialsDto() { Username = "john", Password = ""};
    
        // Act
        var result =  _authController.Register(request);
    
        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = (BadRequestObjectResult)result.Result;
        Assert.Equal("Username and password are required.", badRequestResult.Value);
    }
    
    

    [Fact]
    public async Task Register_Returns_BadRequest_When_Password_Is_Too_Short()
    {
        // Arrange
        var request = new CredentialsDto() { Username = "john", Password = "pp"};

        // Act
        var result = _authController.Register(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = (BadRequestObjectResult)result.Result;
        Assert.Equal("Username and password are required.", badRequestResult.Value);
    }
}