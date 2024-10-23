using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Commands.MovePet;

public class MovePetService(
    IVolunteerRepository repository,
    IValidator<MovePetCommand> validator,
    ILogger<MovePetService> logger,
    IUnitOfWork unitOfWork) : ICommandService<MovePetCommand>
{
    public async Task<Result> ExecuteAsync(
        MovePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await repository.GetByIdAsync(
            command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.ErrorList;

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        var moveResult = volunteerResult.Value.MovePet(petResult.Value, command.Position);
        if (moveResult.IsFailure)
            return moveResult.ErrorList;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Moved pet to position {position}", command.Position);

        return Result.Success();
    }
}