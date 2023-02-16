using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public interface IBoatAppContext :IDisposable
{
    public DbSet<User> Users { get; set; }
    public DbSet<Boat> Boats { get; set; }
}