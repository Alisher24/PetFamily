using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Species.Application.Breeds.Command.Delete;

public class DeleteBreedService(
    ISpeciesRepository speciesRepository,
    IValidator<DeleteBreedCommand> validator,
    ILogger<DeleteBreedService> logger,
    IUnitOfWork unitOfWork,
    IVolunteersContracts volunteersContracts) : ICommandService<DeleteBreedCommand>
{
    public async Task<Result> ExecuteAsync(DeleteBreedCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesResult = await speciesRepository
            .GetByIdAsync(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return Errors.General.NotFound($"Species with id: {command.SpeciesId}");

        var breedResult = speciesResult.Value.GetBreedById(command.BreedId);
        if (breedResult.IsFailure)
            return Errors.General.NotFound($"Breed with id: {command.SpeciesId}");

        var petResult = await volunteersContracts
            .CheckPetAvailabilityByBreedId(command.BreedId, cancellationToken);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        speciesResult.Value.DeletePet(breedResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted breed with id {breedId}", command.BreedId);

        return Result.Success();
    }
}