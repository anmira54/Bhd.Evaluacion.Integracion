using Bhd.Evaluacion.Integracion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserPhone> UsersPhones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
