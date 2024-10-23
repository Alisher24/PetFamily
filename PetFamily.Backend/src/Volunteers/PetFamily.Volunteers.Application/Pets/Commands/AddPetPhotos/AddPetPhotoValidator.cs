using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Dtos.Validators;
using PetFamily.Core.Files;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Commands.AddPetPhotos;

public class AddPetPhotoValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotoValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleForEach(a => a.Photos).SetValidator(new UploadFileDtoValidator
        {
            MaxLength = StreamLengths.MaxPhotoLength
        });
    }
}