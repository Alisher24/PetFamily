using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Volunteers.Commands.UpdateRequisites;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record UpdateRequisitesRequest(
    Guid VolunteerId,
    IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand() =>
        new(VolunteerId, Requisites);
}