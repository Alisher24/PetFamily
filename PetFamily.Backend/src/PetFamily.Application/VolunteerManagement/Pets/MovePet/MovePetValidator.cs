using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.MovePet;

public class MovePetValidator : AbstractValidator<MovePetCommand>
{
    public MovePetValidator()
    {
        RuleFor(m => m.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(m => m.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleFor(m => m.Position).MustBeValueObject(SerialNumber.Create);
    }
}