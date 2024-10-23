using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Species.Queries.GetSpeciesById;

public record GetSpeciesByIdQuery(Guid Id) : IQuery;