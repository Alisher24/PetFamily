using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Application.Models;

namespace Application.VolunteerManagement.Volunteers.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationService(IReadDbContext readDbContext)
    : IQueryService<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    public async Task<PagedList<VolunteerDto>> ExecuteAsync(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = readDbContext.Volunteers;

        return await volunteersQuery.ToPagedListAsync(query.Page, query.PageSize, cancellationToken);
    }
}