using Application.SpeciesManagement;
using Domain.Aggregates.Species;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Shared;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SpeciesRepository(WriteDbContext dbContext) : ISpeciesRepository
{
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
}