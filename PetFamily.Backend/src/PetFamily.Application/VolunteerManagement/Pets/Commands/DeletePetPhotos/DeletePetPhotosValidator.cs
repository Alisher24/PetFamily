using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Commands.DeletePetPhotos;

public class DeletePetPhotosValidator : AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(d => d.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleForEach(d => d.PhotoPaths).MustBeValueObject(p => PhotoPath
            .Create(Path.GetFileNameWithoutExtension(p), Path.GetExtension(p)));
    }
}