using Application.SpeciesManagement.Species.Queries;

namespace PetFamily.API.Controllers.Species.Requests;

public record GetSpeciesWithPaginationRequest(int Page, 
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() => 
        new(Page, PageSize);
}