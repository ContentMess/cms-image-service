using Cms.Shared.Constants;
using Cms.Shared.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.ImageService.Api.Setups;

public static class HealthCheckSetup
{
    public static void SetupHealthCheck(
        this IHealthChecksBuilder builder,
        IConfiguration configuration
    )
    {
        builder
            .AddRabbitMQ(configuration)
            .AddApplicationLifecycleHealthCheck([HealthCheckTag.StartupTag]);
    }
}
