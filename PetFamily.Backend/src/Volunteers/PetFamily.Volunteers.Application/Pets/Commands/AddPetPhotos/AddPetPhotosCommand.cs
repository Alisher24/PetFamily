using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Pets.Commands.AddPetPhotos;

public record AddPetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Photos) : ICommand;