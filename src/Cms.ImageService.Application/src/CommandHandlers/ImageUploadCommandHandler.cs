using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cms.ImageService.Application.CommandHandlers.Interfaces;
using Cms.ImageService.Application.Contracts.Commands;
using Cms.ImageService.Domain.Constants;
using Cms.ImageService.Domain.Entities;
using Cms.ImageService.Infrastructure.Services.Interfaces;

namespace Cms.ImageService.Application.CommandHandlers;

internal sealed class ImageUploadCommandHandler(
    ILocalStorageService localStorageService,
    IExternalStorageService externalStorageService,
    IImageConvertService imageConvertService,
    IDocumentDatabaseService documentDatabaseService
) : IImageUploadCommandHandler
{
    public async Task<ImageUploadCommandResult> HandleAsync(ImageUploadCommand command, CancellationToken cancellationToken)
    {
        string? outputFilePath = null;

        try
        {
            outputFilePath = await imageConvertService.ConvertToWebpAsync(
                command.FileStream,
                100,
                cancellationToken
            );

            var outputFileInfo = new FileInfo(outputFilePath);

            var image = new Image(
                Guid.NewGuid(),
                outputFileInfo.Name,
                outputFileInfo.Length,
                ImageFormat.Webp
            );

            await externalStorageService.UploadImageFileAsync(outputFilePath, cancellationToken);

            await documentDatabaseService.UpsertAsync(image, cancellationToken);

            return new ImageUploadCommandResult(
                image.Id,
                image.FileName,
                image.SizeInBytes,
                image.Format
            );
        }
        finally
        {
            if (outputFilePath is not null)
            {
                localStorageService.DeleteFile(outputFilePath);
            }
        }
    }
}