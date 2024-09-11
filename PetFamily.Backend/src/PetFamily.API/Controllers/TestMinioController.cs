using Application.TestMinio.Requests;
using Application.TestMinio.Services;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;

namespace PetFamily.API.Controllers;

public class TestMinioController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> UploadTestAsync(
        IFormFile file,
        [FromServices] UploadTestService service,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var path = Guid.NewGuid().ToString();

        var request = new UploadTestRequest(stream, "tests", path);

        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult> GetTestAsync(
        [FromQuery] GetTestRequest request,
        [FromServices] GetTestService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpGet("/all")]
    public async Task<ActionResult> GetAllTestAsync(
        [FromQuery] int expiry,
        [FromServices] GetAllTestService service,
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
    public async Task<ActionResult> DeleteTestAsync(
        [FromQuery] DeleteTestRequest request,
        [FromServices] DeleteTestService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }
}