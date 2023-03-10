using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class User
{
    
    public long UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    [Required]
    public List<Boat> Boats { get; set; } = new List<Boat>();
}