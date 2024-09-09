using Application.Volunteer.Requests;
using Domain.Shared;
using FluentValidation;

namespace Application.Volunteer.Validators;

public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}