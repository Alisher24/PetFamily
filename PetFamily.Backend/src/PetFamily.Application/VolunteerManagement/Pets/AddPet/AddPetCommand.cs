using Application.Abstraction;
using Application.Dtos;
using Domain.Enums;

namespace Application.VolunteerManagement.Pets.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
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
    HelpStatuses HelpStatuses,
    IEnumerable<RequisiteDto> Requisites) : ICommand;