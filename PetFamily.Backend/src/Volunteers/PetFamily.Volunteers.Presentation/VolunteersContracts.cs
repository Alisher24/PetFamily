using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityByBreedId;
using PetFamily.Volunteers.Application.Pets.Queries.CheckPetAvailabilityBySpeciesId;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Volunteers.Presentation;

public class VolunteersContracts(
    CheckPetAvailabilityBySpeciesIdService checkPetAvailabilityBySpeciesIdService,
    CheckPetAvailabilityByBreedIdService checkPetAvailabilityByBreedIdService) : IVolunteersContracts
{
    public async Task<Result> CheckPetAvailabilityBySpeciesId(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new CheckPetAvailabilityBySpeciesIdQuery(id);

        var checkPetAvailabilityBySpeciesIdResult =
            await checkPetAvailabilityBySpeciesIdService.ExecuteAsync(query, cancellationToken);

        return checkPetAvailabilityBySpeciesIdResult;
    }

    public async Task<Result> CheckPetAvailabilityByBreedId(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new CheckPetAvailabilityByBreedIdQuery(id);

        var checkPetAvailabilityByBreedIdResult =
            await checkPetAvailabilityByBreedIdService.ExecuteAsync(query, cancellationToken);

        return checkPetAvailabilityByBreedIdResult;
    }
}