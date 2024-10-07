using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Application.SpeciesManagement;
using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Type = Domain.Aggregates.Species.ValueObjects.Type;

namespace Application.VolunteerManagement.Pets.Commands.AddPet;

public class AddPetService(
    IReadDbContext readDbContext,
    IVolunteerRepository volunteerRepository,
    ISpeciesRepository speciesRepository,
    IValidator<AddPetCommand> validator,
    ILogger<AddPetService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, AddPetCommand>
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

        var speciesResult = await speciesRepository.GetByIdAsync(
            readDbContext.Species, command.SpeciesId, cancellationToken);
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
            command.HelpStatuses,
            requisites);

        return pet;
    }
}