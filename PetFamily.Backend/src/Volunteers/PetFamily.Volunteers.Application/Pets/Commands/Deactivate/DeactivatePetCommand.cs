using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Commands.Deactivate;

public record DeactivatePetCommand(Guid VolunteerId, Guid PetId) : ICommand;