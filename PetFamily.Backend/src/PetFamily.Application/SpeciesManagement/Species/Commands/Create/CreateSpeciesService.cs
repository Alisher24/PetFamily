using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.SpeciesManagement.Species.Commands.Create;

public class CreateSpeciesService(
    ISpeciesRepository speciesRepository,
    IValidator<CreateSpeciesCommand> validator,
    ILogger<CreateSpeciesService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, CreateSpeciesCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        CreateSpeciesCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var name = Name.Create(command.Name).Value;
        var speciesNameResult = await speciesRepository.GetByNameAsync(name, cancellationToken);
        if (speciesNameResult.IsSuccess)
            return Errors.General.ValueIsAlreadyExists($"Species with a same name: {name.Value}");

        var speciesId = SpeciesId.NewSpeciesId();
        
        var description = Description.Create(command.Description).Value;

        var species = new Domain.Aggregates.Species.Species(speciesId, name, description);

        await speciesRepository.AddAsync(species, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Created species with id {speciesId}", speciesId.Value);

        return species.Id.Value;
    }
}