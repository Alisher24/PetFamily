using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Pets.Commands.AssignMainPetPhoto;

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