﻿using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.Delete;

public class DeleteVolunteerService(
    IVolunteerRepository volunteerRepository,
    IValidator<DeleteVolunteerCommand> validator,
    ILogger<DeleteVolunteerService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, DeleteVolunteerCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");
        
        volunteerResult.Value.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Deleted volunteer with id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}