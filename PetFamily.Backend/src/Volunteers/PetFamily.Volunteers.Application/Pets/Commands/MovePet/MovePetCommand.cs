using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Commands.MovePet;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int Position) : ICommand;