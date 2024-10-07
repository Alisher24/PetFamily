﻿using Application.Database;
using Application.Dtos;
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

    Task<Result> GetPetBySpeciesId(Guid id,
        IQueryable<PetDto> readPetDbContext,
        CancellationToken cancellationToken = default);

    Task<Result> GetPetByBreedId(Guid id,
        IQueryable<PetDto> readPetDbContext,
        CancellationToken cancellationToken = default);

    Task<Guid> AddAsync(
        Domain.Aggregates.Species.Species species, 
        CancellationToken cancellationToken = default);

    Task<Result> Delete(
        Domain.Aggregates.Species.Species species,
        IReadDbContext readDbContext,
        CancellationToken cancellationToken = default);
}