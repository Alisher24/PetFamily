using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;