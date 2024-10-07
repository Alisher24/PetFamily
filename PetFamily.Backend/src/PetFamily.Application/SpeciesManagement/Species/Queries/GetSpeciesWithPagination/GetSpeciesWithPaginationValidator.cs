using Domain.Shared;
using FluentValidation;

namespace Application.SpeciesManagement.Species.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationValidator : AbstractValidator<GetSpeciesWithPaginationQuery>
{
    public GetSpeciesWithPaginationValidator()
    {
        RuleFor(g => g.Page).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("Page"));
        RuleFor(g => g.PageSize).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}