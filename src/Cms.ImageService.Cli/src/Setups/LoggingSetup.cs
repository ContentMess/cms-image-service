using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

namespace Cms.ImageService.Cli.Setups;

public static class LoggingSetup
{
    public static IServiceCollection SetupLogging(this IServiceCollection services)
    {
        services.AddLogging(builder =>
            builder
                .ClearProviders()
                .AddConsole()
                .AddOpenTelemetry(options =>
                {
                    options.IncludeFormattedMessage = true;
                    options.IncludeScopes = true;
                    options.AddOtlpExporter();
                }));

        return services;
    }
}