using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Species.Contracts;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.ValueObjects;
using Type = PetFamily.Volunteers.Domain.ValueObjects.Type;

namespace PetFamily.Volunteers.Application.Pets.Commands.AddPet;

public class AddPetService(
    IVolunteerRepository volunteerRepository,
    IValidator<AddPetCommand> validator,
    ILogger<AddPetService> logger,
    IUnitOfWork unitOfWork,
    ISpeciesContracts speciesContracts) : ICommandService<Guid, AddPetCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository.GetByIdAsync(
            command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.ErrorList;
        
        var speciesResult = await speciesContracts.GetSpeciesById(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.ErrorList;

        var breedResult = speciesResult.Value.Breeds.FirstOrDefault(b => b.Id == command.BreedId);
        if (breedResult is null)
            return Errors.General.NotFound($"Breed with breedId {command.BreedId}");

        var type = new Type(speciesResult.Value.Id, breedResult.Id);

        var pet = InitPet(command, type);

        volunteerResult.Value.AddPet(pet);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Added pet with id {petId}", pet.Id.Value);

        return pet.Id.Value;
    }

    private Pet InitPet(AddPetCommand command, Type type)
    {
        var petId = PetId.NewPetId();
        var name = Name.Create(command.Name);
        var description = Description.Create(command.Description);
        var color = Color.Create(command.Color);
        var informationHealth = InformationHealth.Create(command.InformationHealth);
        var address = Address.Create(command.Address.City,
            command.Address.District,
            command.Address.Street,
            command.Address.House);

        var weight = Weight.Create(command.Weight);
        var height = Height.Create(command.Height);
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
        var dateOfBirth = DateOfBirth.Create(command.DateOfBirth);
        var helpStatus = Enum.Parse<HelpStatuses>(command.HelpStatus);
        var requisites = new List<Requisite>(command.Requisites
            .Select(r => new Requisite(
                Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        var pet = new Pet(
            petId.Value,
            name.Value,
            description.Value,
            type,
            color.Value,
            informationHealth.Value,
            address.Value,
            weight.Value,
            height.Value,
            phoneNumber.Value,
            command.IsNeutered,
            dateOfBirth.Value,
            command.IsVaccinated,
            helpStatus,
            requisites);

        return pet;
    }
}