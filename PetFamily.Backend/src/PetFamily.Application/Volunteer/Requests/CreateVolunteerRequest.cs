using Domain.Models.Volunteer.Dto;

namespace Application.Volunteer.Requests;

public record CreateVolunteerRequest(FullNameDto FullName,
    string Email,
    string Description,
    int YearsExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto>? SocialNetworks,
    IEnumerable<RequisiteDto>? Requisites);