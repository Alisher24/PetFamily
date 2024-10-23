using PetFamily.Core.Dtos;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Contracts;

public interface ISpeciesContracts
{
    public Task<Result<SpeciesDto>> GetSpeciesById(Guid id, CancellationToken cancellationToken = default);
}