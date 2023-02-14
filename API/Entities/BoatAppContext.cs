using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API;

public class BoatAppContext : DbContext
{
    
    public BoatAppContext(DbContextOptions<BoatAppContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(s => s.Boats)
            .WithOne(e => e.user)
            .HasForeignKey(e => e.userId);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Boat> Boats { get; set; }
}