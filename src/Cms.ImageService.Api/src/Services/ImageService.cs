using System;
using System.Threading.Tasks;
using Cms.ImageService.Application.Contracts.Queries;
using Cms.ImageService.Application.QueryHandlers.Interfaces;
using Cms.Protos;
using Grpc.Core;
using static Cms.Protos.ImageService;

namespace Cms.ImageService.Api.Services;

public class ImageService(
    IImageGetByIdQueryHandler imageGetByIdQueryHandler
) : ImageServiceBase
{
    public override async Task<ImageGetByIdResponse> GetById(ImageGetByIdRequest request, ServerCallContext context)
    {
        if (Guid.TryParse(request.Id, out var id) is false)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid GUID format for Id."));
        }

        var result = await imageGetByIdQueryHandler.HandleAsync(new ImageGetByIdQuery(id), context.CancellationToken) 
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Image with id '{request.Id}' not found"));

        return new ImageGetByIdResponse
        {
            Id = id.ToString(),
            FileName = result.FileName,
            SizeInBytes = result.SizeInBytes,
            Format = result.Format switch 
            {
                Domain.Constants.ImageFormat.Webp => ImageFormat.Webp,
                _ => throw new RpcException(new Status(StatusCode.Internal, "Unknown image format.")),
            }
        };
    }
}
