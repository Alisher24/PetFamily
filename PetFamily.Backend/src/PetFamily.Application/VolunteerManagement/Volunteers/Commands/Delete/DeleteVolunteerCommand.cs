using Application.Abstraction;

namespace Application.VolunteerManagement.Volunteers.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;