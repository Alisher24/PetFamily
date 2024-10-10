using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using Domain.Enums;
using Domain.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Type = Domain.Aggregates.Species.ValueObjects.Type;

namespace Application.VolunteerManagement.Pets.Commands.UpdatePet;

public class UpdatePetService(
    IVolunteerRepository volunteerRepository,
    IReadDbContext readDbContext,
    IValidator<UpdatePetCommand> validator,
    ILogger<UpdatePetService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, UpdatePetCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(UpdatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var species = await readDbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == command.SpeciesId, cancellationToken);
        if (species is null)
            return Errors.General.NotFound($"Species with id: {command.SpeciesId}");
        var breed = species.Breeds.FirstOrDefault(b => b.Id == command.BreedId);
        if (breed is null)
            return Errors.General.NotFound($"Breed with id: {command.SpeciesId}");

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var type = new Type(SpeciesId.Create(command.SpeciesId), BreedId.Create(command.BreedId).Value);
        var color = Color.Create(command.Color).Value;
        var informationHealth = InformationHealth.Create(command.InformationHealth).Value;
        var address = Address.Create(
            command.Address.City,
            command.Address.District,
            command.Address.Street,
            command.Address.House).Value;
        var weight = Weight.Create(command.Weight).Value;
        var height = Height.Create(command.Height).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var dateOfBirth = DateOfBirth.Create(command.DateOfBirth).Value;
        var helpStatus = Enum.Parse<HelpStatuses>(command.HelpStatus);
        var requisites = new List<Requisite>(command.Requisites
            .Select(r => new Requisite(
                Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        var updatePetResult = volunteerResult.Value.UpdatePet(
            command.PetId,
            name,
            description,
            type,
            color,
            informationHealth,
            address,
            weight,
            height,
            phoneNumber,
            command.IsNeutered,
            dateOfBirth,
            command.IsVaccinated,
            helpStatus,
            requisites);
        if (updatePetResult.IsFailure)
            return updatePetResult.ErrorList;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated pet with id {petId}", command.PetId);

        return command.PetId;
    }
}