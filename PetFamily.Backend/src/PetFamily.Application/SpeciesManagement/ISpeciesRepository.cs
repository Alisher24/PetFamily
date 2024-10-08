using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;

namespace Application.SpeciesManagement;

public interface ISpeciesRepository
{
    Task<Result<Domain.Aggregates.Species.Species>> GetByIdAsync(
        SpeciesId speciesId,
        CancellationToken cancellationToken = default);
    
    Task<Result<Domain.Aggregates.Species.Species>> GetByNameAsync(
        Name name,
        CancellationToken cancellationToken = default);

    Task<Guid> AddAsync(
        Domain.Aggregates.Species.Species species, 
        CancellationToken cancellationToken = default);

    void Delete(Domain.Aggregates.Species.Species species);
}