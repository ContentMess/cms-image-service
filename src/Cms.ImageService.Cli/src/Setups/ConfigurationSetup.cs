using Microsoft.Extensions.Configuration;

namespace Cms.ImageService.Cli.Setups;

public static class ConfigurationSetup
{
    public static IConfigurationBuilder ConfigureConfiguration(this IConfigurationBuilder builder, string[] args)
    {
        return builder
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);
    }
}