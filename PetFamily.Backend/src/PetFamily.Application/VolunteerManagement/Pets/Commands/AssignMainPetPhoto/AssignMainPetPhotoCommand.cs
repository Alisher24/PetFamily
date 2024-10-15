using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Commands.AssignMainPetPhoto;

public record AssignMainPetPhotoCommand(Guid VolunteerId, Guid PetId, string PhotoPath) : ICommand;