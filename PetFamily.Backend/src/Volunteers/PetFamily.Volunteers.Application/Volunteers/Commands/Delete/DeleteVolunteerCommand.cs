using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;