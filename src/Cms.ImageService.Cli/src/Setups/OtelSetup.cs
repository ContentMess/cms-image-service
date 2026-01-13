using System;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Cms.ImageService.Cli.Setups;

public static class OtelSetup
{
    public static TracerProvider SetupTraces(IServiceProvider serviceProvider)
    {
        return OpenTelemetry
            .Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault())
            .AddSource(Telemetry.ServiceName)
            .AddHttpClientInstrumentation()
            .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources")
            .AddOtlpExporter()
            .Build();
    }

    public static MeterProvider SetupMetrics()
    {
        return OpenTelemetry
            .Sdk.CreateMeterProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault())
            .AddHttpClientInstrumentation()
            .AddOtlpExporter()
            .Build();
    }
}