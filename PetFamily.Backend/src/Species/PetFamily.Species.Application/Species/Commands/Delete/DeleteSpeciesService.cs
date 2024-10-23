using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Species.Application.Species.Commands.Delete;

public class DeleteSpeciesService(
    ISpeciesRepository speciesRepository,
    IValidator<DeleteSpeciesCommand> validator,
    ILogger<DeleteSpeciesService> logger,
    IUnitOfWork unitOfWork,
    IVolunteersContracts volunteersContracts) : ICommandService<DeleteSpeciesCommand>
{
    public async Task<Result> ExecuteAsync(
        DeleteSpeciesCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesResult = await speciesRepository
            .GetByIdAsync(command.Id, cancellationToken);
        if (speciesResult.IsFailure)
            return Errors.General.NotFound($"Species with id: {command.Id}");

        var petResult = await volunteersContracts
            .CheckPetAvailabilityBySpeciesId(command.Id, cancellationToken);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        speciesRepository.Delete(speciesResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted species with id {speciesId}", command.Id);

        return Result.Success();
    }
}