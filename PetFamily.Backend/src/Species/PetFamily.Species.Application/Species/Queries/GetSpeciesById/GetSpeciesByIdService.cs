using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Application.Species.Queries.GetSpeciesById;

public class GetSpeciesByIdService(
    IReadDbContext readDbContext,
    IValidator<GetSpeciesByIdQuery> validator) : IQueryService<SpeciesDto, GetSpeciesByIdQuery>
{
    public async Task<Result<SpeciesDto>> ExecuteAsync(
        GetSpeciesByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var speciesQuery = await readDbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == query.Id, cancellationToken);
        if (speciesQuery is null)
            return Errors.General.NotFound($"Species with id: {query.Id}");

        return speciesQuery;
    }
}