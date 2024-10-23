using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.UpdateMainInfo;

public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(u => u.Dto).SetValidator(new UpdateMainInfoDtoValidator());
    }
}