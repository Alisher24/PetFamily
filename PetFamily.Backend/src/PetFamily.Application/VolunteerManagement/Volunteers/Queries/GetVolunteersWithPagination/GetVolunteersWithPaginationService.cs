using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Application.Models;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.Queries.GetVolunteersWithPagination;

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