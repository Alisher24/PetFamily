using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Application.Species.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationService(
    IReadDbContext readDbContext,
    IValidator<GetSpeciesWithPaginationQuery> validator)
    : IQueryService<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    public async Task<Result<PagedList<SpeciesDto>>> ExecuteAsync(
        GetSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesQuery = readDbContext.Species.Include(s => s.Breeds);
        
        return await speciesQuery.ToPagedListAsync(query.Page, query.PageSize, cancellationToken);
    }
}