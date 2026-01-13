using System;
using Cms.ImageService.Domain.Constants;

namespace Cms.ImageService.Application.Contracts.Queries;

public sealed record ImageGetByIdQueryResult(
    Guid Id,
    string FileName,
    long SizeInBytes,
    ImageFormat Format
);
