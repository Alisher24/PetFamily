using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.Commands.Delete;

public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}