using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Application.Species.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdService(
    IReadDbContext readDbContext,
    IValidator<GetBreedsBySpeciesIdQuery> validator) : IQueryService<List<BreedDto>, GetBreedsBySpeciesIdQuery>
{
    public async Task<Result<List<BreedDto>>> ExecuteAsync(
        GetBreedsBySpeciesIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesQuery = readDbContext.Species.Include(s => s.Breeds)
            .FirstOrDefault(s => s.Id == query.SpeciesId);
        if (speciesQuery is null)
            return Errors.General.NotFound($"Species with id: {query.SpeciesId}");

        return speciesQuery.Breeds;
    }
}