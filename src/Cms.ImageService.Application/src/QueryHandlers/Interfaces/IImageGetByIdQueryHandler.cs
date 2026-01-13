using System.Threading;
using System.Threading.Tasks;
using Cms.ImageService.Application.Contracts.Queries;

namespace Cms.ImageService.Application.QueryHandlers.Interfaces;

public interface IImageGetByIdQueryHandler
{
    Task<ImageGetByIdQueryResult?> HandleAsync(ImageGetByIdQuery query, CancellationToken cancellationToken);
}