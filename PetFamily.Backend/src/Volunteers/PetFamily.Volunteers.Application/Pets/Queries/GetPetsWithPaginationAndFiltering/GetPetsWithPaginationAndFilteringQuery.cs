using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos.Filters;

namespace PetFamily.Volunteers.Application.Pets.Queries.GetPetsWithPaginationAndFiltering;

public record GetPetsWithPaginationAndFilteringQuery(
    int Page,
    int PageSize,
    PetsFilterDto? PetsFilter,
    string? SortBy,
    bool? SortAsc) : IQuery;