using Application.Abstraction;

namespace Application.SpeciesManagement.Species.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(int Page, 
    int PageSize) : IQuery;