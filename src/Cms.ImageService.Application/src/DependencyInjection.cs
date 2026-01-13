using Cms.ImageService.Application.CommandHandlers;
using Cms.ImageService.Application.CommandHandlers.Interfaces;
using Cms.ImageService.Application.QueryHandlers;
using Cms.ImageService.Application.QueryHandlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.ImageService.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        // Command Handlers
        services.AddSingleton<IImageUploadCommandHandler, ImageUploadCommandHandler>();

        // Query Handlers
        services.AddSingleton<IImageGetByIdQueryHandler, ImageGetByIdQueryHandler>();
    }
}
