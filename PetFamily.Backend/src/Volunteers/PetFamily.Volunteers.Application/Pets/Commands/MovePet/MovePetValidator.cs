using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Pets.Commands.MovePet;

public class MovePetValidator : AbstractValidator<MovePetCommand>
{
    public MovePetValidator()
    {
        RuleFor(m => m.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(m => m.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleFor(m => m.Position).MustBeValueObject(Position.Create);
    }
}