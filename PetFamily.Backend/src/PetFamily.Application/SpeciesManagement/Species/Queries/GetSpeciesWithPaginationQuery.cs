using Application.Abstraction;

namespace Application.SpeciesManagement.Species.Queries;

public record GetSpeciesWithPaginationQuery(int Page, 
    int PageSize) : IQuery;