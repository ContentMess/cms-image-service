using Cms.ImageService.Domain.Constants;
using Cms.ImageService.Domain.Entities.Base;

namespace Cms.ImageService.Domain.Entities;

public record Image(Guid Id, string FileName, long SizeInBytes, ImageFormat Format)
    : BaseEntity(Id);
