using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Dtos;
using PetFamily.Framework;
using PetFamily.Volunteers.Application.Pets.Commands.AddPet;
using PetFamily.Volunteers.Application.Pets.Commands.AddPetPhotos;
using PetFamily.Volunteers.Application.Pets.Commands.AssignMainPetPhoto;
using PetFamily.Volunteers.Application.Pets.Commands.Deactivate;
using PetFamily.Volunteers.Application.Pets.Commands.Delete;
using PetFamily.Volunteers.Application.Pets.Commands.DeletePetPhotos;
using PetFamily.Volunteers.Application.Pets.Commands.MovePet;
using PetFamily.Volunteers.Application.Pets.Commands.UpdatePet;
using PetFamily.Volunteers.Application.Pets.Commands.UpdatePetStatus;
using PetFamily.Volunteers.Application.Volunteers.Commands.Create;
using PetFamily.Volunteers.Application.Volunteers.Commands.Delete;
using PetFamily.Volunteers.Application.Volunteers.Commands.UpdateMainInfo;
using PetFamily.Volunteers.Application.Volunteers.Commands.UpdateRequisites;
using PetFamily.Volunteers.Application.Volunteers.Commands.UpdateSocialNetworks;
using PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteerById;
using PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PetFamily.Volunteers.Presentation.Processors;
using PetFamily.Volunteers.Presentation.Volunteers.Requests;

namespace PetFamily.Volunteers.Presentation.Volunteers;

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

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/main-photo")]
    public async Task<ActionResult> AssignMainPetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] string photoPath,
        [FromServices] AssignMainPetPhotoService service,
        CancellationToken cancellationToken)
    {
        var command = new AssignMainPetPhotoCommand(volunteerId, petId, photoPath);
        
        var result = await service.ExecuteAsync(command, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }
    
    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/deactivate")]
    public async Task<ActionResult> DeactivatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeactivatePetService service,
        CancellationToken cancellationToken)
    {
        var command = new DeactivatePetCommand(volunteerId, petId);

        var result = await service.ExecuteAsync(command, cancellationToken);
        if (result.IsFailure)
            return result.ErrorList.ToResponse();

        return Ok();
    }
    
    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}")]
    public async Task<ActionResult> DeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetService service,
        CancellationToken cancellationToken)
    {
        var command = new DeletePetCommand(volunteerId, petId);

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