using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Test;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using API;
using API.Controllers;
using API.Services;
using API.Models;

public class AuthControllerTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder().Build();
    private readonly Mock<IUserService> _userService = new Mock<IUserService>();
    private readonly BoatAppContext _context = new BoatAppContext(new DbContextOptionsBuilder<BoatAppContext>()
        .UseInMemoryDatabase(databaseName: "BoatAppTestDb")
        .Options);
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authController = new AuthController(_configuration, _userService.Object, _context);
    }

    [Fact]
    public void GetBoats_ReturnsBadRequest_WhenNoUserExists()
    {
        // Arrange
        _userService.Setup(x => x.GetMyName()).Returns("non-existing-user");
            
        // Act
        var result = _authController.GetBoats();
            
        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void Register_ReturnsConflict_WhenUserExists()
    {
        // Arrange
        _context.Users.Add(new User { Username = "existing-user" });
        _context.SaveChanges();
        var request = new CredentialsDto { Username = "existing-user", Password = "password" };

        // Act
        var result = _authController.Register(request);

        // Assert
        Assert.IsType<ConflictObjectResult>(result.Result);
    }

    [Fact]
    public void Login_ReturnsBadRequest_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new CredentialsDto { Username = "non-existing-user", Password = "password" };

        // Act
        var result = _authController.Login(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}}