using System.Text.Json.Serialization;
using Cms.ImageService.Api.Setups;
using Cms.ImageService.Application;
using Cms.ImageService.Infrastructure;
using Cms.Shared.Setups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.ImageService.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.SetupOpenTelemetry();
        builder.Services.SetupOpenTelemetry();

        builder.Services.AddGrpc();

        var healthCheckBuilder = builder.Services.AddHealthChecks();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration, healthCheckBuilder);

        builder.Services.AddOpenApi();

        builder
            .Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    // show enum value in swagger.
                    new JsonStringEnumConverter()
                );
            });

        healthCheckBuilder.SetupHealthCheck(builder.Configuration);

        using var app = builder.Build();

        app.SetupApplication();

        app.MapOpenApi();
        app.UseScalar();

        app.UseStaticFiles();

        // app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseHealthCheck();

        app.MapControllers();

        app.Run();
    }

    private static WebApplication SetupApplication(this WebApplication app)
    {
        app.MapGrpcService<Services.ImageService>();

        app.UseHealthCheck();

        return app;
    }
}
