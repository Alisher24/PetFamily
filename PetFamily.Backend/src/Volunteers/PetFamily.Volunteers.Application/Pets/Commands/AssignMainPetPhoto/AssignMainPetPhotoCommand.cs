using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Commands.AssignMainPetPhoto;

public record AssignMainPetPhotoCommand(Guid VolunteerId, Guid PetId, string PhotoPath) : ICommand;