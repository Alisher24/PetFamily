using Application.Volunteer.Dto;
using Application.Volunteer.Requests;
using Application.Volunteer.Services;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;

namespace PetFamily.API.Controllers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerService service,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.ExecuteAsync(request, cancellationToken);

        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoDto dto,
        [FromServices] UpdateMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMainInfoRequest(id, dto);

        var result = await service.ExecuteAsync(request, cancellationToken);
        
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] IEnumerable<SocialNetworkDto> socialNetworks,
        [FromServices] UpdateSocialNetworksService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateSocialNetworksRequest(id, socialNetworks);
        
        var result = await service.ExecuteAsync(request, cancellationToken);
        
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] IEnumerable<RequisiteDto> requisites,
        [FromServices] UpdateRequisitesService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateRequisitesRequest(id, requisites);
        
        var result = await service.ExecuteAsync(request, cancellationToken);
        
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteVolunteerRequest(id);
        
        var result = await service.ExecuteAsync(request, cancellationToken);
        
        if (result.IsFailure)
            return result.ErrorList.ToResponse();
 
        return Ok(result.Value);
    }
}