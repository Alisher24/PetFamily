using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application;

public interface IVolunteerRepository
{
    Task<Guid> AddAsync(Volunteer volunteer,
        CancellationToken cancellationToken = default);

    Guid Delete(Volunteer volunteer,
        CancellationToken cancellationToken = default);

    Guid Save(Volunteer volunteer, 
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer>> GetByIdAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer>> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer>> GetByPhoneNumberAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default);
}