using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityByBreedId;

public record CheckPetAvailabilityByBreedIdQuery(Guid Id) : IQuery;