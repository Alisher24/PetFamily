using Application.Database;
using Application.Dtos;
using Application.SpeciesManagement;
using Domain.Aggregates.Species;
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

    public async Task<Result> Delete(Species species,
        IReadDbContext readDbContext,
        CancellationToken cancellationToken = default)
    {
        var petResult = await GetPetBySpeciesId(species.Id.Value, readDbContext.Pets, cancellationToken);
        if (petResult.IsSuccess)
            return Errors.General.ValueIsBeingUsedByAnotherObject($"Species with id: {species.Id.Value}");

        dbContext.Species.Remove(species);

        return Result.Success();
    }

    public async Task<Result> GetPetBySpeciesId(Guid id,
        IQueryable<PetDto> readPetDbContext,
        CancellationToken cancellationToken = default)
    {
        var petResult = await readPetDbContext
            .FirstOrDefaultAsync(p => p.SpeciesId == id, cancellationToken);

        return petResult is null
            ? Errors.General.NotFound($"Pet with speciesId {id}")
            : Result.Success();
    }

    public async Task<Result> GetPetByBreedId(Guid id,
        IQueryable<PetDto> readPetDbContext,
        CancellationToken cancellationToken = default)
    {
        var petResult = await readPetDbContext
            .FirstOrDefaultAsync(p => p.BreedId == id, cancellationToken);

        return petResult is null
            ? Errors.General.NotFound($"Pet with breedId {id}")
            : Result.Success();
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

    public async Task<Result<SpeciesDto>> GetByIdAsync(
        IQueryable<SpeciesDto> readSpeciesDbContext,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var speciesResult = await readSpeciesDbContext
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        
        return speciesResult is null
            ? Errors.General.NotFound($"Species with speciesId {id}")
            : speciesResult;
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