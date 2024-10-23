using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Queries.GetPetsWithPaginationAndFiltering;

public class GetPetsWithPaginationAndFilteringValidator : AbstractValidator<GetPetsWithPaginationAndFilteringQuery>
{
    public GetPetsWithPaginationAndFilteringValidator()
    {
        RuleFor(g => g.Page).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("Page"));
        RuleFor(g => g.PageSize).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("PageSize"));
        RuleFor(g => g.SortBy).Must(s => s is null || !string.IsNullOrWhiteSpace(s))
            .WithError(Errors.General.ValueIsInvalid("SortBy"));
        RuleFor(g => g).Must(s =>
                (s.SortBy is null && s.SortAsc is null)
                || (s.SortBy is not null && s.SortAsc is not null))
            .WithError(Errors.General.ValueIsInvalid("SortAsc"));
    }
}