using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;

namespace PetFamily.Species.Application.Species.Commands.Create;

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

        var species = new Domain.Species(speciesId, name, description);

        await speciesRepository.AddAsync(species, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Created species with id {speciesId}", speciesId.Value);

        return species.Id.Value;
    }
}