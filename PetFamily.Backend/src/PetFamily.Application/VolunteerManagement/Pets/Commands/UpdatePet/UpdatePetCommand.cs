using Application.Abstraction;
using Application.Dtos;
using Domain.Enums;

namespace Application.VolunteerManagement.Pets.Commands.UpdatePet;

public record UpdatePetCommand(
    Guid VolunteerId,
    Guid PetId,
    string Name,
    string Description,
    Guid SpeciesId,
    Guid BreedId,
    string Color,
    string InformationHealth,
    AddressDto Address,
    double Weight,
    double Height,
    string PhoneNumber,
    bool IsNeutered,
    DateOnly DateOfBirth,
    bool IsVaccinated,
    HelpStatuses HelpStatus,
    IEnumerable<RequisiteDto> Requisites) : ICommand;