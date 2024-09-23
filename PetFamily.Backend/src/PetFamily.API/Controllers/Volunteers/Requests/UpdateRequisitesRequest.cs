﻿using Application.Dtos;
using Application.VolunteerManagement.Volunteers.UpdateRequisites;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record UpdateRequisitesRequest(
    Guid VolunteerId,
    IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand() =>
        new(VolunteerId, Requisites);
}