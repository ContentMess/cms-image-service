using System;
using System.CommandLine;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Cms.ImageService.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cms.ImageService.Cli.Commands;

internal static class SetupDocumentCollections
{
    private const string CmdName = "setup-document-collections";

    public static Command CreateCommand(IServiceProvider services)
    {
        var cmd = new Command(CmdName, "Setup document collections.");

        cmd.SetAction((parsedResult, cancellationToken) => Handler(parsedResult, services, cancellationToken));

        return cmd;
    }

    internal static async Task Handler(ParseResult parseResult, IServiceProvider services, CancellationToken cancellationToken)
    {
        var activityName = string.Join(" ", parseResult.Tokens);
        using var activity = Telemetry.ActivitySource.StartActivity(activityName, ActivityKind.Internal);

        using var serviceScope = services.CreateScope();

        var documentDatabaseService = serviceScope.ServiceProvider.GetRequiredService<IDocumentDatabaseService>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Command>>();

        await documentDatabaseService.SetupCollections();
    }
}