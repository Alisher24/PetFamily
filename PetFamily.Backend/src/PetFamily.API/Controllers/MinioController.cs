using Application.TestMinio.Requests;
using Application.TestMinio.Services;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;

namespace PetFamily.API.Controllers;

public class MinioController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> UploadAsync(
        IFormFile file,
        [FromServices] UploadService service,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var path = Guid.NewGuid() + "." + file.ContentType.Split('/').Last();

        var request = new UploadRequest(stream, "tests", path);

        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync(
        [FromQuery] GetRequest request,
        [FromServices] GetService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("/all")]
    public async Task<ActionResult> GetAllAsync(
        [FromQuery] int expiry,
        [FromServices] GetAllService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.ExecuteAsync(expiry, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value.Count == 1
            ? result.Value
            : result.Value.Select(x => x));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(
        [FromQuery] DeleteRequest request,
        [FromServices] DeleteService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }
}