using PetFamily.Core.Dtos.Filters;
using PetFamily.Volunteers.Application.Pets.Queries.GetPetsWithPaginationAndFiltering;

namespace PetFamily.Volunteers.Presentation.Pets.Requests;

public record GetPetsWithPaginationAndFilteringRequest(
    int Page,
    int PageSize,
    PetsFilterDto? PetsFilter,
    string? SortBy,
    bool? SortAsc)
{
    public GetPetsWithPaginationAndFilteringQuery ToQuery() =>
        new(Page,
            PageSize,
            PetsFilter,
            SortBy,
            SortAsc);
}