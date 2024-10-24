﻿using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Volunteers.Commands.Create;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    int YearsExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<RequisiteDto> Requisites)
{
    public CreateVolunteerCommand ToCommand() =>
        new(FullName,
            Email,
            Description,
            YearsExperience,
            PhoneNumber,
            SocialNetworks,
            Requisites);
}