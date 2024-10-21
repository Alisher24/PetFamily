using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.Delete;

public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}