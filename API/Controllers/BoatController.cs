using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.Models;
using API.Services;
using Newtonsoft.Json;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoatController : ControllerBase
{
    private readonly BoatAppContext _context;
    private readonly IUserService _userService;
   // private readonly IBoatService _boatService;

    public BoatController(BoatAppContext context, IUserService userService)
    {
        _userService = userService;
        _context = context;
     //   _boatService = boatService;
    }

    [HttpPost("add"), Authorize]
    public async Task<ActionResult<UserDto>> AddBoat(BoatDto newBoat)
    {
        // return _boatService.AddBoat(boat);
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        Boat boat = new Boat();
        boat.name = newBoat.name;
        boat.description = newBoat.description;
        boat.image_url = newBoat.image_url;
        user.Boats.Add(boat);
        _context.SaveChanges();
        UserDto userDto = new UserDto()
        {
            username = user.Username,
            boats = _context.Boats.Where(b => b.userId == user.UserId).ToList()
        };
        return Ok(userDto);
    }

    [HttpPost("delete"), Authorize]
    public async Task<ActionResult<UserDto>> DeleteBoat(BoatDto requestedBoat)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        var boats = _context.Boats.Where(b => b.userId == user.UserId).ToList();
        var itemToRemove = boats.Find(b => b.name == requestedBoat.name);
        user.Boats.Remove(itemToRemove);
        _context.SaveChanges();
        UserDto userDto = new UserDto()
        {
            username = user.Username,
            boats = _context.Boats.Where(b => b.userId == user.UserId).ToList()
        };
        return Ok(userDto);
    }

    [HttpPut("edit"), Authorize]
    public async Task<ActionResult<User>> EditBoat(Boat requestedBoat)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        var boat = user.Boats.FirstOrDefault(e => e.name == requestedBoat.name);
        if (boat == null)
            return NotFound("Boat not found.");
        boat.name = requestedBoat.name;
        boat.description = requestedBoat.description;
        _context.SaveChanges();
        return Ok(user);
    }
}