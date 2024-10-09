using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Application.Models;
using Domain.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.SpeciesManagement.Species.Queries.GetSpeciesWithPagination;

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