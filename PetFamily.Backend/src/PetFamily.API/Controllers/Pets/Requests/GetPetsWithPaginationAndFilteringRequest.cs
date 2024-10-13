using Application.Dtos.Filters;
using Application.VolunteerManagement.Pets.Queries.GetPetsWithPaginationAndFiltering;

namespace PetFamily.API.Controllers.Pets.Requests;

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