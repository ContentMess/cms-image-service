using System.Diagnostics;

namespace Cms.ImageService.Cli;

public static class Telemetry
{
    public static readonly string ServiceName = "Cms.ImageService.Cli";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}
