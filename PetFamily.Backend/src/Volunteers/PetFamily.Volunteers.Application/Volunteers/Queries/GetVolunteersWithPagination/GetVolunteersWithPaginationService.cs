using FluentValidation;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationService(
    IReadDbContext readDbContext,
    IValidator<GetVolunteersWithPaginationQuery> validator)
    : IQueryService<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    public async Task<Result<PagedList<VolunteerDto>>> ExecuteAsync(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteersQuery = readDbContext.Volunteers;

        return await volunteersQuery.ToPagedListAsync(query.Page, query.PageSize, cancellationToken);
    }
}