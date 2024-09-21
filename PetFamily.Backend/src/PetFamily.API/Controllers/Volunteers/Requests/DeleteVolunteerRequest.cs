using Application.VolunteerManagement.Volunteers.Delete;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand() => new(VolunteerId);
}