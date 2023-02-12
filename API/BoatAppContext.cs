using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API;

public class BoatAppContext : DbContext
{
    
    public BoatAppContext(DbContextOptions<BoatAppContext> options) : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.UseSerialColumns();
    // }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Boat> Boats { get; set; }
}