using Application.Dtos.Validators;
using Application.Files;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.AddPetPhotos;

public class AddPetPhotoValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotoValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleForEach(a => a.Photos).SetValidator(new UploadFileDtoValidator()
        {
            MaxLength = StreamLengths.MaxPhotoLength
        });
    }
}