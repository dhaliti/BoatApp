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
    private IConfiguration _configuration;
    private IUserService _userService;
    private BoatAppContext _context;
    private AuthController _authController;

    [SetUp]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder().Build();
        _userService = new UserService(new HttpContextAccessor());
        _context = new BoatAppContext(new DbContextOptionsBuilder<BoatAppContext>()
            .UseInMemoryDatabase(databaseName: "BoatApp")
            .Options);
        _authController = new AuthController(_configuration, _userService, _context);
    }

    [Test]
    public void Login_ReturnsBadRequest_WhenUserNotFound()
    {
        // Arrange
        var request = new CredentialsDto
        {
            Username = "user",
            Password = "password"
        };

        // Act
        var result = _authController.Login(request).Result;

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        Assert.AreEqual("User not found.", ((BadRequestObjectResult)result.Result).Value);
    }

    [Test]
    public void Login_ReturnsBadRequest_WhenWrongPassword()
    {
        // Arrange
        var user = new User
        {
            Username = "user",
            PasswordHash = new byte[] { 1, 2, 3 },
            PasswordSalt = new byte[] { 4, 5, 6 }
        };
        _context.Users.Add(user);
        _context.SaveChanges();

        var request = new CredentialsDto
        {
            Username = "user",
            Password = "wrongpassword"
        };

        // Act
        var result = _authController.Login(request).Result;

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        Assert.AreEqual("Wrong password.", ((BadRequestObjectResult)result.Result).Value);
    }

    [Test]
    public void Register_ReturnsConflict_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Username = "user"
        };
        _context.Users.Add(user);
        _context.SaveChanges();

        var request = new CredentialsDto
        {
            Username = "user",
            Password = "password"
        };

        // Act
        var result = _authController.Register(request).Result;

        // Assert
        Assert.IsInstanceOf<ConflictObjectResult>(result);
        Assert.AreEqual("User already exists.", ((ConflictObjectResult)result).Value);
    }
}