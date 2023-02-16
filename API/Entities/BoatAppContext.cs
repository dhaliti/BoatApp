using API;
using API.Models;
using Microsoft.EntityFrameworkCore;

public BoatAppContext(DbContextOptions<BoatAppContext> options) : base(options)
{
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