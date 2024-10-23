using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Commands.Delete;

public record DeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;