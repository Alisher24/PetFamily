using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Pets.MovePet;

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