using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Domain;

namespace PetFamily.Volunteers.Application.Pets.Commands.UpdatePetStatus;

public class UpdatePetStatusService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdatePetStatusCommand> validator,
    ILogger<UpdatePetStatusService> logger,
    IUnitOfWork unitOfWork) : ICommandService<UpdatePetStatusCommand>
{
    public async Task<Result> ExecuteAsync(
        UpdatePetStatusCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var helpStatus = Enum.Parse<HelpStatuses>(command.HelpStatuses);
        
        var updateResult = volunteerResult.Value.UpdatePetStatus(command.PetId, helpStatus);
        if (updateResult.IsFailure)
            return updateResult.ErrorList;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Updated status of pet with id {petId}", command.PetId);

        return Result.Success();
    }
}