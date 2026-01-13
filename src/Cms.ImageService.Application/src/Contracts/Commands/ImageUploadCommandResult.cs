using System;
using Cms.ImageService.Domain.Constants;

namespace Cms.ImageService.Application.Contracts.Commands;

public sealed record ImageUploadCommandResult(
    Guid Id,
    string FileName,
    long SizeInBytes,
    ImageFormat Format
);
