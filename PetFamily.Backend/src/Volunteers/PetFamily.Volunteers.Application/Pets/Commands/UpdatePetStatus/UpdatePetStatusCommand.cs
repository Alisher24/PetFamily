using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Commands.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string HelpStatuses) : ICommand;