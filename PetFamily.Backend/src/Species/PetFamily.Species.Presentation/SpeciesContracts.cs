using PetFamily.Core.Dtos;
using PetFamily.SharedKernel.Shared;
using PetFamily.Species.Application.Species.Queries.GetSpeciesById;
using PetFamily.Species.Contracts;

namespace PetFamily.Species.Presentation;

public class SpeciesContracts(GetSpeciesByIdService service) : ISpeciesContracts
{
    public async Task<Result<SpeciesDto>> GetSpeciesById(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetSpeciesByIdQuery(id);
        
        var speciesResult = await service.ExecuteAsync(query, cancellationToken);

        return speciesResult;
    }
}