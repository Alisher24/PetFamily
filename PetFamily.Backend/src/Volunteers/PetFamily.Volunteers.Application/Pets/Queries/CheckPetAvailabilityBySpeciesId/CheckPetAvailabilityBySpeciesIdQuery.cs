using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityBySpeciesId;

public record CheckPetAvailabilityBySpeciesIdQuery(Guid Id) : IQuery;