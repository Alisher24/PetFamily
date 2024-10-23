using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityBySpeciesId;

public class CheckPetAvailabilityBySpeciesIdService(
    IReadDbContext readDbContext,
    IValidator<CheckPetAvailabilityBySpeciesIdQuery> validator)
    : IQueryService<Result, CheckPetAvailabilityBySpeciesIdQuery>
{
    public async Task<Result<Result>> ExecuteAsync(
        CheckPetAvailabilityBySpeciesIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var petResult = await readDbContext.Pets
            .FirstOrDefaultAsync(p => p.SpeciesId == query.Id, cancellationToken);
        return petResult is not null
            ? Errors.General.ValueIsBeingUsedByAnotherObject($"Pet with speciesId {query.Id}")
            : Result.Success();
    }
}