using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;

namespace PetFamily.Species.Application;

public interface ISpeciesRepository
{
    Task<Result<Domain.Species>> GetByIdAsync(
        SpeciesId speciesId,
        CancellationToken cancellationToken = default);
    
    Task<Result<Domain.Species>> GetByNameAsync(
        Name name,
        CancellationToken cancellationToken = default);

    Task<Guid> AddAsync(
        Domain.Species species, 
        CancellationToken cancellationToken = default);

    void Delete(Domain.Species species);
}