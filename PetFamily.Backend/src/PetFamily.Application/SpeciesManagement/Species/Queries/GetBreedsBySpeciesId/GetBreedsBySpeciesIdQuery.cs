using Application.Abstraction;

namespace Application.SpeciesManagement.Species.Queries.GetBreedsBySpeciesId;

public record GetBreedsBySpeciesIdQuery(Guid SpeciesId) : IQuery;