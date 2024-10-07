using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
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

        var deleteResult = await speciesRepository
            .Delete(speciesResult.Value, readDbContext, cancellationToken);
        if (deleteResult.IsFailure)
            return deleteResult.ErrorList;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Deleted species with id {speciesId}", command.Id);

        return Result.Success();
    }
}