using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Pets.Commands.UpdatePet;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record UpdatePetRequest(
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
    string HelpStatus,
    IEnumerable<RequisiteDto> Requisites)
{
    public UpdatePetCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId,
            petId,
            Name,
            Description,
            SpeciesId,
            BreedId,
            Color,
            InformationHealth,
            Address,
            Weight,
            Height,
            PhoneNumber,
            IsNeutered,
            DateOfBirth,
            IsVaccinated,
            HelpStatus,
            Requisites);
}