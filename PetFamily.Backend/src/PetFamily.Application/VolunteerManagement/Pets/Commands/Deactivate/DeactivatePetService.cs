using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Pets.Commands.Deactivate;

public class DeactivatePetService(
    IVolunteerRepository volunteerRepository,
    IValidator<DeactivatePetCommand> validator,
    ILogger<DeactivatePetService> logger,
    IUnitOfWork unitOfWork) : ICommandService<DeactivatePetCommand>
{
    public async Task<Result> ExecuteAsync(
        DeactivatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        petResult.Value.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deactivated pet with id {petId}", command.PetId);

        return Result.Success();
    }
}