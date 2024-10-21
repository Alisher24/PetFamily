using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Species.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(int Page, 
    int PageSize) : IQuery;