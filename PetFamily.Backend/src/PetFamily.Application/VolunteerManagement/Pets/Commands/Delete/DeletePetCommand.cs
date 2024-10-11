using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Commands.Delete;

public record DeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;