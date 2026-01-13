using System.Threading;
using System.Threading.Tasks;
using Cms.ImageService.Application.CommandHandlers.Interfaces;
using Cms.ImageService.Application.Contracts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.ImageService.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ImageController(IImageUploadCommandHandler imageUploadCommandHandler) : ControllerBase
{
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload(
        [FromForm] IFormFile file,
        CancellationToken cancellationToken
    )
    {
        using var fileStream = file.OpenReadStream();

        var request = new ImageUploadCommand(fileStream, file.FileName);

        var response = await imageUploadCommandHandler.HandleAsync(request, cancellationToken);

        return Ok(response);
    }
}
