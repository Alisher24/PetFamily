using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;