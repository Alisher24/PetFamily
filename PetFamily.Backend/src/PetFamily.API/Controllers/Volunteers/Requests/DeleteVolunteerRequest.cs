using Application.VolunteerManagement.Volunteers.Commands.Delete;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand() => new(VolunteerId);
}