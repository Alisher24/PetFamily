using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Commands.DeletePetPhotos;

public record DeletePetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<string> PhotoPaths) : ICommand;