using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cms.ImageService.Cli.Setups;
using Cms.ImageService.Application;
using Cms.ImageService.Infrastructure;
using System.CommandLine;
using System.CommandLine.Invocation;
using Cms.ImageService.Cli.Commands;

namespace Cms.ImageService.Cli;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .ConfigureConfiguration(args)
            .Build();

        var collection = new ServiceCollection();
        collection.SetupLogging();

        collection.AddSingleton<IConfiguration>(configuration);
        collection.AddApplication();
        collection.AddInfrastructure(configuration);

        using var serviceProvider = collection.BuildServiceProvider();

        using var meterProvider = OtelSetup.SetupMetrics();
        using var traceProvider = OtelSetup.SetupTraces(serviceProvider);

        var parseResult = new RootCommand("Cms.ImageService.Cli")
        {
            SetupDocumentCollections.CreateCommand(serviceProvider),
        }.Parse(args);

        if (parseResult.Action is ParseErrorAction errorAction)
        {
            errorAction.ShowHelp = true;
            errorAction.ShowTypoCorrections = true;
        }

        return await parseResult.InvokeAsync();
    }
}