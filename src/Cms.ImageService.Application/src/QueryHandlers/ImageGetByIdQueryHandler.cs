using System.Threading;
using System.Threading.Tasks;
using Cms.ImageService.Application.Contracts.Queries;
using Cms.ImageService.Application.QueryHandlers.Interfaces;
using Cms.ImageService.Domain.Entities;
using Cms.ImageService.Infrastructure.Services.Interfaces;
using MongoDB.Driver.Linq;

namespace Cms.ImageService.Application.QueryHandlers;

internal sealed class ImageGetByIdQueryHandler(IDocumentDatabaseService documentDatabaseService) : IImageGetByIdQueryHandler
{
    public async Task<ImageGetByIdQueryResult?> HandleAsync(ImageGetByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await documentDatabaseService
            .GetQueryable<Image>()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        return result is null
            ? null
            : new ImageGetByIdQueryResult(
                result.Id,
                result.FileName,
                result.SizeInBytes,
                result.Format
            );
    }
}