using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Volunteers.Commands.UpdateMainInfo;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record UpdateMainInfoRequest(
    Guid VolunteerId,
    UpdateMainInfoDto Dto)
{
    public UpdateMainInfoCommand ToCommand() => new(VolunteerId, Dto);
}