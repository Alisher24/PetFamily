using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Application.Species.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdValidator : AbstractValidator<GetBreedsBySpeciesIdQuery>
{
    public GetBreedsBySpeciesIdValidator()
    {
        RuleFor(g => g.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsInvalid("SpeciesId"));
    }
}