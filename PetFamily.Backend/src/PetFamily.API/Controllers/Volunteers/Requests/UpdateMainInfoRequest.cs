using Application.Dtos;
using Application.VolunteerManagement.Volunteers.UpdateMainInfo;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record UpdateMainInfoRequest(
    Guid VolunteerId,
    UpdateMainInfoDto Dto)
{
    public UpdateMainInfoCommand ToCommand() => new(VolunteerId, Dto);
}