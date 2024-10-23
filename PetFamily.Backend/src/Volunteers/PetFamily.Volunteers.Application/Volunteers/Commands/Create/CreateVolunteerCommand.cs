using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    int YearsExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<RequisiteDto> Requisites) : ICommand;