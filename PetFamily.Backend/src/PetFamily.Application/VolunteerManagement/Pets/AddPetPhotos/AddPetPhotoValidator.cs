using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.AddPetPhotos;

public class AddPetPhotoValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotoValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.Photos).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}