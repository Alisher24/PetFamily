using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Commands.Delete;

public class DeletePetValidator : AbstractValidator<DeletePetCommand>
{
    public DeletePetValidator()
    {
        RuleFor(s => s.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(s => s.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
    }
}