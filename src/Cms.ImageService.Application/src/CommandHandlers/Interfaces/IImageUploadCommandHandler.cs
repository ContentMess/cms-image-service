using System.Threading;
using System.Threading.Tasks;
using Cms.ImageService.Application.Contracts.Commands;

namespace Cms.ImageService.Application.CommandHandlers.Interfaces;

public interface IImageUploadCommandHandler
{
    Task<ImageUploadCommandResult> HandleAsync(ImageUploadCommand command, CancellationToken cancellationToken);
}