using Application.Abstraction;

namespace Application.VolunteerManagement.Volunteers.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;