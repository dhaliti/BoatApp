namespace API;

public class BoatDto
{
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    
    public string? newName { get; set; }
    public string image_url { get; set; } = string.Empty;
}
