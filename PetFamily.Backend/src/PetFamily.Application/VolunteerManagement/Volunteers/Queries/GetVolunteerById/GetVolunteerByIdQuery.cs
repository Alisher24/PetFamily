using Application.Abstraction;

namespace Application.VolunteerManagement.Volunteers.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;