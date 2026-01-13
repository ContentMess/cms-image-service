using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace Cms.ImageService.Api.Setups;

public static class ScalarSetup
{
    public static void UseScalar(this WebApplication app)
    {
        app.MapScalarApiReference(opts =>
        {
            opts.Title = "ImageService API Reference";
        });
    }
}