using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Species.Application.Breeds.Command;
using PetFamily.Species.Application.Breeds.Command.Delete;
using PetFamily.Species.Application.Species.Commands.Create;
using PetFamily.Species.Application.Species.Commands.Delete;
using PetFamily.Species.Application.Species.Queries.GetBreedsBySpeciesId;
using PetFamily.Species.Application.Species.Queries.GetSpeciesWithPagination;
using PetFamily.Species.Presentation.Requests;

namespace PetFamily.Species.Presentation;

public class SpeciesController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetSpeciesWithPaginationRequest request,
        [FromServices] GetSpeciesWithPaginationService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToQuery(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result);
    }

    [HttpGet("{id:guid}/breeds")]
    public async Task<ActionResult> GetBreeds(
        [FromRoute] Guid id,
        [FromServices] GetBreedsBySpeciesIdService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(new GetBreedsBySpeciesIdQuery(id), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateSpeciesService service,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/breed")]
    public async Task<ActionResult> AddBreed(
        [FromRoute] Guid id,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToCommand(id), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesService service,
        CancellationToken cancellationToken)
    {
        var request = new DeleteSpeciesCommand(id);

        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }

    [HttpDelete("{speciesId:guid}/breeds/{breedId:guid}")]
    public async Task<ActionResult> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedService service,
        CancellationToken cancellationToken)
    {
        var request = new DeleteBreedCommand(speciesId, breedId);

        var result = await service.ExecuteAsync(request, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }
}