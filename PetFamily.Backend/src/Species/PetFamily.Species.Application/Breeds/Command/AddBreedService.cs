﻿using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Species.Domain.Entities;

namespace PetFamily.Species.Application.Breeds.Command;

public class AddBreedService(
    ISpeciesRepository speciesRepository,
    IValidator<AddBreedCommand> validator,
    ILogger<AddBreedService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, AddBreedCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        AddBreedCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesResult = await speciesRepository
            .GetByIdAsync(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.ErrorList;

        var name = Name.Create(command.Name).Value;
        if (speciesResult.Value.GetBreedByName(name).IsSuccess)
            return Errors.General.ValueIsAlreadyExists($"Breed with a same name: {name.Value}");

        var breedId = BreedId.NewBreedId();
        
        var description = Description.Create(command.Description).Value;

        var breed = new Breed(breedId, name, description);
            
        speciesResult.Value.AddBread(breed);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Added breed with id {breedId}", breed.Id.Value);

        return breed.Id.Value;
    }
}