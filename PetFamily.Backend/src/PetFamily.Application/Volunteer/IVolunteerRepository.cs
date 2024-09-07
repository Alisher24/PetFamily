using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.Shared;

namespace Application.Volunteer;

public interface IVolunteerRepository
{
    Task<Guid> AddAsync(Domain.Aggregates.Volunteer.Volunteer volunteer,
        CancellationToken cancellationToken = default);

    Task<Guid> SaveAsync(Domain.Aggregates.Volunteer.Volunteer volunteer, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Aggregates.Volunteer.Volunteer>> GetByIdAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Aggregates.Volunteer.Volunteer>> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Aggregates.Volunteer.Volunteer>> GetByPhoneNumberAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default);
}