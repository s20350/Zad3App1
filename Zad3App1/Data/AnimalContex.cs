using Microsoft.EntityFrameworkCore;
using Zad3App1.Model;

namespace Zad3App1.Data;

public class AnimalContext : DbContext
{
    public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }
}