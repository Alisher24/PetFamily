using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Application.Species.Queries.GetSpeciesById;

public class GetSpeciesByIdValidator : AbstractValidator<GetSpeciesByIdQuery>
{
    public GetSpeciesByIdValidator()
    {
        RuleFor(g => g.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
    }
}