using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Species.Queries.GetBreedsBySpeciesId;

public record GetBreedsBySpeciesIdQuery(Guid SpeciesId) : IQuery;