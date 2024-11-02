using Bhd.Evaluacion.Integracion.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyVaultIfConfigured(builder.Configuration);
builder.Services.AddJsons(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddWebServices();

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi(settings =>
    {
        settings.Path = "/api";
        settings.DocumentPath = "/api/specification.json";
    });
}

if (app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            if (ctx.File.Name == "specification.json")
            {
                ctx.Context.Response.StatusCode = StatusCodes.Status404NotFound;
                ctx.Context.Response.ContentLength = 0;
                ctx.Context.Response.Body = Stream.Null;
            }
        }
    });
}
else
{
    app.UseStaticFiles();
}

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));

app.MapEndpoints();

await app.RunAsync();

