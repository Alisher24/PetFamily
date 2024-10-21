using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure.DbContexts;

namespace PetFamily.Species.Infrastructure;

public class SpeciesRepository(WriteDbContext dbContext) : ISpeciesRepository
{
    public async Task<Guid> AddAsync(Domain.Species species,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Species.AddAsync(species, cancellationToken);

        return species.Id.Value;
    }

    public void Delete(Domain.Species species) => dbContext.Species.Remove(species);

    public async Task<Result<Domain.Species>> GetByIdAsync(SpeciesId speciesId,
        CancellationToken cancellationToken = default)
    {
        var species = await dbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == speciesId,
                cancellationToken);

        if (species is null)
            return Errors.General.NotFound(speciesId.Value.ToString());

        return species;
    }

    public async Task<Result<Domain.Species>> GetByNameAsync(Name name,
        CancellationToken cancellationToken = default)
    {
        var species = await dbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Name == name,
                cancellationToken);

        if (species is null)
            return Errors.General.NotFound(name.Value.ToString());

        return species;
    }
}