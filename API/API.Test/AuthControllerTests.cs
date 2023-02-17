using API;
using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

/*
 I didn't have much time left for unit testing and I encountered an issue with the DBContext.
 Neither FakeItEasy nor Moq seemed to mock it properly. 
 In my opinion, it might be better for unit-testing purposes to instantiate the DbContext directly in the controller 
 by using something like 'using (var conn = new NpgsqlConnection(connectionString)) {}
 */

public class AuthControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
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
    public void Register_Returns_BadRequest_When_Password_Is_Too_Short()
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