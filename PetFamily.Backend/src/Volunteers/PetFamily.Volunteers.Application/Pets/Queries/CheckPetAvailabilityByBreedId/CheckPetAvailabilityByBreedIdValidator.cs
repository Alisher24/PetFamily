using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityBySpeciesId;

namespace PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityByBreedId;

public class CheckPetAvailabilityByBreedIdValidator : AbstractValidator<CheckPetAvailabilityBySpeciesIdQuery>
{
    public CheckPetAvailabilityByBreedIdValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
    }
}