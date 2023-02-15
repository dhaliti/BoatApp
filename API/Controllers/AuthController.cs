using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.Models;
using API.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly BoatAppContext _context;

    public AuthController(IConfiguration configuration, IUserService userService, BoatAppContext context)
    {
        _configuration = configuration;
        _userService = userService;
        _context = context;
    }

    [HttpGet, Authorize]
    public ActionResult<string> GetBoats()
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        UserDto userDto = new UserDto()
        {
            username = user.Username,
            boats = _context.Boats.Where(b => b.userId == user.UserId).ToList()
        };
        return Ok(userDto);    
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(CredentialsDto request)
    {
        User user = new User();
        var userExists = _context.Users.Any(u => u.Username == request.Username);
        if (userExists)
            return Conflict("User already exists.");
        Console.WriteLine(request.ToString());
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(CredentialsDto request)
    {
        var userExists = _context.Users.FirstOrDefault(u => u.Username == request.Username);
        if (userExists != null)
        {
            if (!VerifyPasswordHash(request.Password, userExists.PasswordHash, userExists.PasswordSalt))
            {
                Console.WriteLine("Wrong password.");
                return BadRequest("Wrong password.");
            }
            string token = CreateToken(userExists);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, ref userExists);
            _context.SaveChanges();
            UserDto user = new UserDto()
            {
                username = userExists.Username,
                boats = _context.Boats.Where(b => b.userId == userExists.UserId).ToList()
            };
            return Ok(new {
                user, token
            });
        }
        return BadRequest("User not found.");
    }

    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };

        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken newRefreshToken, ref User userExists)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        userExists.RefreshToken = newRefreshToken.Token;
        userExists.TokenCreated = newRefreshToken.Created;
        userExists.TokenExpires = newRefreshToken.Expires;
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}