using Domain.Shared;
using FluentValidation;

namespace Application.SpeciesManagement.Species.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdValidator : AbstractValidator<GetBreedsBySpeciesIdQuery>
{
    public GetBreedsBySpeciesIdValidator()
    {
        RuleFor(g => g.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsInvalid("SpeciesId"));
    }
}