using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    int Page, 
    int PageSize) : IQuery;