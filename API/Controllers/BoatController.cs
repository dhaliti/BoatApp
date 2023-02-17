using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.Entities;
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

    public BoatController(BoatAppContext? context, IUserService userService)
    {
        _userService = userService;
        _context = context;
    }

    [HttpPost("add"), Authorize]
    public async Task<ActionResult<UserDto>> AddBoat(BoatDto newBoat)
    {
        if (newBoat.name.IsNullOrEmpty())
            return BadRequest("Name is required.");
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        var boats = _context.Boats.Where(b => b.userId == user.UserId).ToList();
        if (boats.Any(b => b.name == newBoat.name))
            return BadRequest("Boat with this name already exists.");
        Boat boat = new Boat()
        {
            name = newBoat.name,
            description = newBoat.description,
            image_url = newBoat.image_url
        };
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
    public async Task<ActionResult<User>> EditBoat(BoatDto requestedBoat)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        var boats = _context.Boats.Where(b => b.userId == user.UserId).ToList();
        if (boats.FirstOrDefault(b=>b.name == requestedBoat.newName) != null)
            return BadRequest("Boat already exists");
        var itemToEdit = boats.FirstOrDefault(b => b.name == requestedBoat.name);
        if (itemToEdit == null)
            return NotFound("Boat not found.");
        itemToEdit.name = requestedBoat.newName.IsNullOrEmpty() ? requestedBoat.name : requestedBoat.newName;
        itemToEdit.description = requestedBoat.description;
        itemToEdit.image_url = requestedBoat.image_url;
        _context.SaveChanges();
        UserDto userDto = new UserDto()
        {
            username = user.Username,
            boats = _context.Boats.Where(b => b.userId == user.UserId).ToList()
        };
        return Ok(userDto);
    }
}