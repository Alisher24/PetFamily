using Application.Abstraction;
using Application.Dtos.Filters;

namespace Application.VolunteerManagement.Pets.Queries.GetPetsWithPaginationAndFiltering;

public record GetPetsWithPaginationAndFilteringQuery(
    int Page,
    int PageSize,
    PetsFilterDto? PetsFilter,
    string? SortBy,
    bool? SortAsc) : IQuery;