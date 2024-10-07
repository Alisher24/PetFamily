using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.SpeciesManagement.Breeds.Command.Delete;

public class DeleteBreedService(
    ISpeciesRepository speciesRepository,
    IReadDbContext readDbContext,
    IValidator<DeleteBreedCommand> validator,
    ILogger<DeleteBreedService> logger,
    IUnitOfWork unitOfWork) : ICommandService<DeleteBreedCommand>
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
        
        var petResult = await speciesRepository
            .GetPetByBreedId(command.BreedId, readDbContext.Pets, cancellationToken);
        if (petResult.IsSuccess)
            return Errors.General.ValueIsBeingUsedByAnotherObject($"Breed with id: {command.BreedId}");
        
        speciesResult.Value.DeletePet(breedResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Deleted breed with id {breedId}", command.BreedId);

        return Result.Success();
    }
}