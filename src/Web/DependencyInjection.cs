using Azure.Identity;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces;
using Bhd.Evaluacion.Integracion.Web.Services;
using Microsoft.AspNetCore.Mvc;

using NSwag;
using NSwag.Generation.Processors.Security;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddSwagger();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "Bhd.Evaluacion.Integracion API";

            configure.AddSecurity("JWT", [], new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["External:KeyVault:BaseUrl"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }

    public static IServiceCollection AddJsons(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddJsonFile("messages.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables();

        configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables();

        return services;
    }
}
