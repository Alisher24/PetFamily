using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Pets.Commands.DeletePetPhotos;

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