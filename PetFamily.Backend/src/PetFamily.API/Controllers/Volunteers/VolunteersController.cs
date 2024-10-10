using Application.Dtos;
using Application.VolunteerManagement.Pets.Commands.AddPet;
using Application.VolunteerManagement.Pets.Commands.AddPetPhotos;
using Application.VolunteerManagement.Pets.Commands.DeletePetPhotos;
using Application.VolunteerManagement.Pets.Commands.MovePet;
using Application.VolunteerManagement.Pets.Commands.UpdatePet;
using Application.VolunteerManagement.Pets.Commands.UpdatePetStatus;
using Application.VolunteerManagement.Volunteers.Commands.Create;
using Application.VolunteerManagement.Volunteers.Commands.Delete;
using Application.VolunteerManagement.Volunteers.Commands.UpdateMainInfo;
using Application.VolunteerManagement.Volunteers.Commands.UpdateRequisites;
using Application.VolunteerManagement.Volunteers.Commands.UpdateSocialNetworks;
using Application.VolunteerManagement.Volunteers.Queries.GetVolunteerById;
using Application.VolunteerManagement.Volunteers.Queries.GetVolunteersWithPagination;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Processors;

namespace PetFamily.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationService service,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var result = await service.ExecuteAsync(query, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetVolunteerById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdService service,
        CancellationToken cancellationToken)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await service.ExecuteAsync(query, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerService service,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoDto dto,
        [FromServices] UpdateMainInfoService service,
        CancellationToken cancellationToken)
    {
        var request = new UpdateMainInfoRequest(id, dto);

        var result = await service.ExecuteAsync(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] IEnumerable<SocialNetworkDto> socialNetworks,
        [FromServices] UpdateSocialNetworksService service,
        CancellationToken cancellationToken)
    {
        var request = new UpdateSocialNetworksRequest(id, socialNetworks);

        var result = await service.ExecuteAsync(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] IEnumerable<RequisiteDto> requisites,
        [FromServices] UpdateRequisitesService service,
        CancellationToken cancellationToken)
    {
        var request = new UpdateRequisitesRequest(id, requisites);

        var result = await service.ExecuteAsync(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerService service,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerRequest(id);

        var result = await service.ExecuteAsync(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToCommand(id), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<ActionResult> AddPetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        IFormFileCollection photos,
        [FromServices] AddPetPhotosService service,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(photos);

        var command = new AddPetPhotosCommand(volunteerId, petId, fileDtos);

        var result = await service.ExecuteAsync(command, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/movement")]
    public async Task<ActionResult> MovePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] int position,
        [FromServices] MovePetService service,
        CancellationToken cancellationToken)
    {
        var command = new MovePetCommand(volunteerId, petId, position);

        var result = await service.ExecuteAsync(command, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}")]
    public async Task<ActionResult> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetRequest request,
        [FromServices] UpdatePetService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(request.ToCommand(volunteerId, petId), cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/help-status")]
    public async Task<ActionResult> UpdatePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] string helpStatuses,
        [FromServices] UpdatePetStatusService service,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePetStatusCommand(volunteerId, petId, helpStatuses);
        
        var result = await service.ExecuteAsync(command, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<ActionResult> DeletePetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] IEnumerable<string> paths,
        [FromServices] DeletePetPhotosService service,
        CancellationToken cancellationToken)
    {
        var command = new DeletePetPhotosCommand(volunteerId, petId, paths);

        var result = await service.ExecuteAsync(command, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }
}