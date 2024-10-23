using PetFamily.Volunteers.Application.Volunteers.Commands.Delete;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand() => new(VolunteerId);
}