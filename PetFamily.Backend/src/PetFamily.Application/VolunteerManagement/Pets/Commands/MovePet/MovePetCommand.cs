using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Commands.MovePet;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int Position) : ICommand;