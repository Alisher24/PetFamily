using System.Linq.Expressions;
using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Dtos.Filters;
using Application.Extensions;
using Application.Models;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Queries.GetPetsWithPaginationAndFiltering;

public class GetPetsWithPaginationAndFilteringService(
    IReadDbContext readDbContext,
    IValidator<GetPetsWithPaginationAndFilteringQuery> queryValidator,
    IValidator<PetsFilterDto> filterValidator)
    : IQueryService<PagedList<PetDto>, GetPetsWithPaginationAndFilteringQuery>
{
    public async Task<Result<PagedList<PetDto>>> ExecuteAsync(
        GetPetsWithPaginationAndFilteringQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryValidationResult = await queryValidator.ValidateAsync(query, cancellationToken);
        if (queryValidationResult.IsValid == false)
            return queryValidationResult.ToErrorList();

        var petsQuery = readDbContext.Pets;

        if (query.PetsFilter is not null)
        {
            var filterValidationResult = await filterValidator.ValidateAsync(query.PetsFilter, cancellationToken);
            if (filterValidationResult.IsValid == false)
                return filterValidationResult.ToErrorList();
                
            petsQuery = FilteringPet(petsQuery, query.PetsFilter);
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy) && query.SortAsc is not null)
        {
            var selector = SortSelector(query.SortBy);

            petsQuery = query.SortAsc == true
                ? petsQuery.OrderBy(selector)
                : petsQuery.OrderByDescending(selector);
        }

        return await petsQuery
            .ToPagedListAsync(query.Page, query.PageSize, cancellationToken);
    }

    private IQueryable<PetDto> FilteringPet(IQueryable<PetDto> petsQuery, PetsFilterDto petsFilter)
    {
        petsQuery = petsQuery.WhereIf(
            petsFilter.Id is not null,
            p => p.Id == petsFilter.Id);

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(petsFilter.Name),
            p => p.Name.Contains(petsFilter.Name!));

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(petsFilter.Description),
            p => p.Description.Contains(petsFilter.Description!));

        petsQuery = petsQuery.WhereIf(
            petsFilter.SpeciesId is not null,
            p => p.SpeciesId == petsFilter.SpeciesId);

        petsQuery = petsQuery.WhereIf(
            petsFilter.BreedId is not null,
            p => p.BreedId == petsFilter.BreedId);

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(petsFilter.Color),
            p => p.Color.Contains(petsFilter.Color!));

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(petsFilter.InformationHealth),
            p => p.InformationHealth.Contains(petsFilter.InformationHealth!));

        petsQuery = petsQuery.WhereIf(
            petsFilter.WeightFrom is not null,
            p => p.Weight >= petsFilter.WeightFrom);

        petsQuery = petsQuery.WhereIf(
            petsFilter.WeightTo is not null,
            p => p.Weight <= petsFilter.WeightTo);

        petsQuery = petsQuery.WhereIf(
            petsFilter.HeightFrom is not null,
            p => p.Height >= petsFilter.HeightFrom);

        petsQuery = petsQuery.WhereIf(
            petsFilter.HeightTo is not null,
            p => p.Height <= petsFilter.HeightTo);

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(petsFilter.PhoneNumber),
            p => p.PhoneNumber.Contains(petsFilter.PhoneNumber!));

        petsQuery = petsQuery.WhereIf(
            petsFilter.IsNeutered is not null,
            p => p.IsNeutered == petsFilter.IsNeutered);

        petsQuery = petsQuery.WhereIf(
            petsFilter.DateOfBirthFrom is not null,
            p => p.DateOfBirth >= petsFilter.DateOfBirthFrom);

        petsQuery = petsQuery.WhereIf(
            petsFilter.DateOfBirthTo is not null,
            p => p.DateOfBirth <= petsFilter.DateOfBirthTo);

        petsQuery = petsQuery.WhereIf(
            petsFilter.IsVaccinated is not null,
            p => p.IsVaccinated == petsFilter.IsVaccinated);

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(petsFilter.HelpStatus),
            p => p.HelpStatus.ToString() == petsFilter.HelpStatus!);

        petsQuery = petsQuery.WhereIf(
            petsFilter.CreatedAtFrom is not null,
            p => p.CreatedAt >= petsFilter.CreatedAtFrom);

        petsQuery = petsQuery.WhereIf(
            petsFilter.CreatedAtTo is not null,
            p => p.CreatedAt <= petsFilter.CreatedAtTo);

        petsQuery = petsQuery.WhereIf(
            petsFilter.PositionFrom is not null,
            p => p.Position >= petsFilter.PositionFrom);

        petsQuery = petsQuery.WhereIf(
            petsFilter.PositionTo is not null,
            p => p.Position <= petsFilter.PositionTo);

        return petsQuery;
    }

    private Expression<Func<PetDto, object>> SortSelector(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "name" => p => p.Name,
            "dateofbirth" => p => p.DateOfBirth,
            "speciesid" => p => p.SpeciesId,
            "breedid" => p => p.Id,
            "color" => p => p.Color,
            "city" => p => p.Address.City,
            "district" => p => p.Address.District,
            "street" => p => p.Address.Street,
            "house" => p => p.Address.House,
            _ => p => p.VolunteerId,
        };
    }
}