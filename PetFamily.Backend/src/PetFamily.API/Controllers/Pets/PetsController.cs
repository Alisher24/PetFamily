using Application.VolunteerManagement.Pets.Queries.GetPetsWithPaginationAndFiltering;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Pets.Requests;
using PetFamily.API.Extensions;

namespace PetFamily.API.Controllers.Pets;

public class PetsController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> GetAll(
        [FromQuery] GetPetsWithPaginationAndFilteringRequest request,
        [FromServices] GetPetsWithPaginationAndFilteringService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToQuery(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();
        
        return Ok(result.Value);
    }
}