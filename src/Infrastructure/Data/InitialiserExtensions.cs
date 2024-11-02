using Bhd.Evaluacion.Integracion.Domain.Entities;
using Bhd.Evaluacion.Integracion.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var countries = new List<Country>
        {
            new() { Code = "AR", Description = "Argentina" },
            new() { Code = "BR", Description = "Brazil" },
            new() { Code = "CL", Description = "Chile" },
            new() { Code = "DO", Description = "Dominican Republic" },
        };

        await _context.Countries.AddRangeAsync(countries);
        await _context.SaveChangesAsync();

        var cities = new List<City>
        {
            new() { Code = "AR-BA", Description = "Buenos Aires", CountryCode = "AR" },
            new() { Code = "AR-CO", Description = "Cordoba", CountryCode = "AR" },
            new() { Code = "BR-SP", Description = "Sao Paulo", CountryCode = "BR" },
            new() { Code = "BR-RJ", Description = "Rio de Janeiro", CountryCode = "BR" },
            new() { Code = "CL-B", Description = "Bogota", CountryCode = "CL" },
            new() { Code = "DO-S", Description = "Santo Domingo", CountryCode = "DO" },
            new() { Code = "DO-P", Description = "Puerto Plata", CountryCode = "DO" },
        };

        await _context.Cities.AddRangeAsync(cities);
        await _context.SaveChangesAsync();
    }
}
