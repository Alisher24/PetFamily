using Application.Abstraction;

namespace Application.VolunteerManagement.Volunteers.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    int Page, 
    int PageSize) : IQuery;