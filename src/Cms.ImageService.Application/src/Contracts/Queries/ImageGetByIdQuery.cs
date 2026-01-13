using System;

namespace Cms.ImageService.Application.Contracts.Queries;

public sealed record ImageGetByIdQuery(Guid Id);