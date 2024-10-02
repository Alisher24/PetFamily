using Application.Abstraction;
using Application.Dtos;

namespace Application.VolunteerManagement.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId,
    IEnumerable<RequisiteDto> Requisites) : ICommand;