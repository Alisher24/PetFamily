using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Commands.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string HelpStatuses) : ICommand;