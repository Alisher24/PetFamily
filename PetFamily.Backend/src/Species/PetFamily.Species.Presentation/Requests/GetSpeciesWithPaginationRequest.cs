using PetFamily.Species.Application.Species.Queries.GetSpeciesWithPagination;

namespace PetFamily.Species.Presentation.Requests;

public record GetSpeciesWithPaginationRequest(int Page, 
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() => 
        new(Page, PageSize);
}