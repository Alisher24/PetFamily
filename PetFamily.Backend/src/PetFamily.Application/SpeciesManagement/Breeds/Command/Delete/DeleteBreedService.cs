using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

        var petResult = await CheckPetAvailabilityByBreedId(command.BreedId, readDbContext.Pets, cancellationToken);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        speciesResult.Value.DeletePet(breedResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted breed with id {breedId}", command.BreedId);

        return Result.Success();
    }

    private async Task<Result> CheckPetAvailabilityByBreedId(
        Guid id,
        IQueryable<PetDto> readPetDbContext,
        CancellationToken cancellationToken = default)
    {
        var petResult = await readPetDbContext
            .FirstOrDefaultAsync(p => p.BreedId == id, cancellationToken);

        return petResult is not null
            ? Errors.General.ValueIsBeingUsedByAnotherObject($"Breed with id {id}")
            : Result.Success();
    }
}