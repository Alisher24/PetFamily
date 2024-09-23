﻿using Application.Dtos;
using Application.VolunteerManagement.Pets.AddPet;
using Domain.Enums;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record AddPetRequest(
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
    IEnumerable<RequisiteDto> Requisites)
{
    public AddPetCommand ToCommand(Guid volunteerId) =>
        new(volunteerId,
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
            HelpStatuses,
            Requisites);
}