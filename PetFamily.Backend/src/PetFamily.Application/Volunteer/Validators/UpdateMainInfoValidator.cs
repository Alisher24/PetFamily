using Application.Volunteer.Requests;
using Domain.Shared;
using FluentValidation;

namespace Application.Volunteer.Validators;

public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(u => u.Dto).SetValidator(new UpdateMainInfoDtoValidator());
    }
}