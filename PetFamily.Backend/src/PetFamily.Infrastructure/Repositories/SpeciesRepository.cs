using Application.SpeciesManagement;
using Domain.Aggregates.Species;
using Domain.Aggregates.Species.Entities;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SpeciesRepository(WriteDbContext dbContext) : ISpeciesRepository
{
    public async Task<Guid> AddAsync(Species species,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Species.AddAsync(species, cancellationToken);

        return species.Id.Value;
    }

    public async Task<Result<Species>> GetByIdAsync(SpeciesId speciesId,
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

    public async Task<Result<Species>> GetByNameAsync(Name name,
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