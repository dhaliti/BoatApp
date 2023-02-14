namespace API;

public class UserDto
{
    public string username { get; set; } = string.Empty;
    public List<Boat> boats = new List<Boat>();
}