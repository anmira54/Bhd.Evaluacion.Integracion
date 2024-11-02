using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;
using Bhd.Evaluacion.Integracion.Domain.Constants;
using Bhd.Evaluacion.Integracion.Infrastructure.Contexts;
using Bhd.Evaluacion.Integracion.Infrastructure.Data;
using Bhd.Evaluacion.Integracion.Infrastructure.Data.Repositories;
using Bhd.Evaluacion.Integracion.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) => options.UseInMemoryDatabase("Application"));

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddInfrastructureRepositories();

        services.AddServices();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder()
            .AddPolicy(Policies.HaveFullAccess, policy => policy.RequireRole(Roles.Administrator));

        return services;
    }

    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPhoneRepository, UserPhoneRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEncryptionService, EncryptionService>();

        return services;
    }
}
