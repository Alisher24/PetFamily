using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationValidator : AbstractValidator<GetVolunteersWithPaginationQuery>
{
    public GetVolunteersWithPaginationValidator()
    {
        RuleFor(g => g.Page).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("Page"));
        RuleFor(g => g.PageSize).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}