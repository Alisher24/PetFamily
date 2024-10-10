using Domain.Enums;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Commands.UpdatePetStatus;

public class UpdatePetStatusValidator : AbstractValidator<UpdatePetStatusCommand>
{
    public UpdatePetStatusValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleFor(u => u.HelpStatuses).NotEmpty()
            .Must(h => Enum.TryParse<HelpStatuses>(h, out _))
            .WithError(Errors.General.ValueIsInvalid("HelpStatuses"));
    }
}