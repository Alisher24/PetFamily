using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Shared;

namespace Application.SpeciesManagement;

public interface ISpeciesRepository
{
    Task<Result<Domain.Aggregates.Species.Species>> GetByIdAsync(
        SpeciesId speciesId,
        CancellationToken cancellationToken = default);
}