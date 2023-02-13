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
    private readonly IBoatService _boatService;

    public BoatController(BoatAppContext context, IUserService userService, IBoatService boatService)
    {
        _userService = userService;
        _context = context;
        _boatService = boatService;
    }

    [HttpPost("create"), Authorize]
    public async Task<ActionResult<User>> AddBoat(Boat boat)
    {
       // return _boatService.AddBoat(boat);
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        user.Boats.Add(boat);
        _context.SaveChanges();
        return CreatedAtAction(JsonConvert.SerializeObject(user), user);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<User>> DeleteBoat(Boat requestedBoat)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == _userService.GetMyName());
        if (user == null)
            return NotFound("User not found.");
        var boat = user.Boats.FirstOrDefault(e => e.name == requestedBoat.name);
        if (boat == null)
            return NotFound("Boat not found.");
        user.Boats.Remove(boat);
        _context.SaveChanges();
        return Ok(user);
    }

    [HttpPut("edit")]
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