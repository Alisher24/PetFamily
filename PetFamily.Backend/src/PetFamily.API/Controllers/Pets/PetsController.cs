﻿using Application.VolunteerManagement.Pets.Queries.GetPetById;
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
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(
        [FromRoute] Guid id,
        [FromServices] GetPetByIdService service,
        CancellationToken cancellationToken)
    {
        var query = new GetPetByIdQuery(id);
        
        var result = await service.ExecuteAsync(query, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();
        
        return Ok(result.Value);
    }
}