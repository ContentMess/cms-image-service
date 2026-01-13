using System.IO;

namespace Cms.ImageService.Application.Contracts.Commands;

public sealed record ImageUploadCommand(Stream FileStream, string FileName);
