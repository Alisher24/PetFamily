using Application.VolunteerManagement.Volunteers.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record GetVolunteersWithPaginationRequest(
    int Page, 
    int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() => 
        new(Page, PageSize);
}