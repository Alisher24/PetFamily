using Application.Dtos.Validators;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.Commands.UpdateMainInfo;

public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(u => u.Dto).SetValidator(new UpdateMainInfoDtoValidator());
    }
}