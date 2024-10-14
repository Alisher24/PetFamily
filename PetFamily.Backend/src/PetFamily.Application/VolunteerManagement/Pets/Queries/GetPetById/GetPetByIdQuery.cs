using Application.Abstraction;

namespace Application.VolunteerManagement.Pets.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;