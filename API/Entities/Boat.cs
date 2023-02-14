using API.Models;

namespace API;

public class Boat

{
    public long boatId { get; set; }
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    
    public string image_url { get; set; } = string.Empty;
    
    public long? userId { get; set; }
    public User user   { get; set; }
}