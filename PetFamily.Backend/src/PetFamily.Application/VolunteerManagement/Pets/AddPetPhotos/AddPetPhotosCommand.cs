using Application.Dtos;

namespace Application.VolunteerManagement.Pets.AddPetPhotos;

public record AddPetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Photos);