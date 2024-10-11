using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Commands.Deactivate;

public record DeactivatePetCommand(Guid VolunteerId, Guid PetId) : ICommand;