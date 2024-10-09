using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.SpeciesManagement.Species.Commands.Delete;

public class DeleteSpeciesService(
    ISpeciesRepository speciesRepository,
    IReadDbContext readDbContext,
    IValidator<DeleteSpeciesCommand> validator,
    ILogger<DeleteSpeciesService> logger,
    IUnitOfWork unitOfWork) : ICommandService<DeleteSpeciesCommand>
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

        var petResult = await CheckPetAvailabilityBySpeciesId(command.Id, readDbContext.Pets, cancellationToken);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        speciesRepository.Delete(speciesResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted species with id {speciesId}", command.Id);

        return Result.Success();
    }

    private async Task<Result> CheckPetAvailabilityBySpeciesId(
        Guid id,
        IQueryable<PetDto> readPetDbContext,
        CancellationToken cancellationToken = default)
    {
        var petResult = await readPetDbContext
            .FirstOrDefaultAsync(p => p.SpeciesId == id, cancellationToken);

        return petResult is not null
            ? Errors.General.ValueIsBeingUsedByAnotherObject($"Pet with speciesId {id}")
            : Result.Success();
    }
}