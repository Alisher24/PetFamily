using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Commands.AssignMainPetPhoto;

public class AssignMainPetPhotoValidator : AbstractValidator<AssignMainPetPhotoCommand>
{
    public AssignMainPetPhotoValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleFor(a => a.PhotoPath)
            .MustBeValueObject(p => PhotoPath.Create(Path.GetFileNameWithoutExtension(p), Path.GetExtension(p)));
    }
}