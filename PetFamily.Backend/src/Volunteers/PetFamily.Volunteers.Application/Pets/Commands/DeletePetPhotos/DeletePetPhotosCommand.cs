using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Commands.DeletePetPhotos;

public record DeletePetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<string> PhotoPaths) : ICommand;