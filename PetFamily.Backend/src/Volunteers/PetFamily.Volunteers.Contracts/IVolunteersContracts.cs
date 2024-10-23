using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Contracts;

public interface IVolunteersContracts
{
    public Task<Result> CheckPetAvailabilityBySpeciesId(Guid id, CancellationToken cancellationToken = default);
    
    public Task<Result> CheckPetAvailabilityByBreedId(Guid id, CancellationToken cancellationToken = default);
}