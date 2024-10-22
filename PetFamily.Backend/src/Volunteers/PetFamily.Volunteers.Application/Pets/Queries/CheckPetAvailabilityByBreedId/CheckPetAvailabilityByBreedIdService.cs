using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityBySpeciesId;

namespace PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityByBreedId;

public class CheckPetAvailabilityByBreedIdService(
    IReadDbContext readDbContext,
    IValidator<CheckPetAvailabilityBySpeciesIdQuery> validator)
    : IQueryService<Result, CheckPetAvailabilityByBreedIdQuery>
{
    public async Task<Result<Result>> ExecuteAsync(
        CheckPetAvailabilityByBreedIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var petResult = await readDbContext.Pets
            .FirstOrDefaultAsync(p => p.BreedId == query.Id, cancellationToken);
        return petResult is not null
            ? Errors.General.ValueIsBeingUsedByAnotherObject($"Pet with breedId {query.Id}")
            : Result.Success();
    }
}