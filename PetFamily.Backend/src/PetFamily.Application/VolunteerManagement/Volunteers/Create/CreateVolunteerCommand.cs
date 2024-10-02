using Application.Abstraction;
using Application.Dtos;

namespace Application.VolunteerManagement.Volunteers.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    int YearsExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<RequisiteDto> Requisites) : ICommand;