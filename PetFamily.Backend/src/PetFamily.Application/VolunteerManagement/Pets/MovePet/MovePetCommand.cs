namespace Application.VolunteerManagement.Pets.MovePet;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int Position);