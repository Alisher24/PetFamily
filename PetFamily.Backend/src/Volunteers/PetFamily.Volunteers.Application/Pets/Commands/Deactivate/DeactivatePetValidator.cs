using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Commands.Deactivate;

public class DeactivatePetValidator : AbstractValidator<DeactivatePetCommand>
{
    public DeactivatePetValidator()
    {
        RuleFor(s => s.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(s => s.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
    }
}