using Application.Abstraction;
using Application.Dtos;

namespace Application.VolunteerManagement.Pets.Commands.AddPetPhotos;

public record AddPetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Photos) : ICommand;