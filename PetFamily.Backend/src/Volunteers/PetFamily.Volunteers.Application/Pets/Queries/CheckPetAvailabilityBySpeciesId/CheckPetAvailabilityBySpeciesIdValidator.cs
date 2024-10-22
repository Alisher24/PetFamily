using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityBySpeciesId;

public class CheckPetAvailabilityBySpeciesIdValidator : AbstractValidator<CheckPetAvailabilityBySpeciesIdQuery>
{
    public CheckPetAvailabilityBySpeciesIdValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
    }
}