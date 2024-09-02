using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;

namespace Application.Volunteer;

public interface IVolunteerRepository
{
    Task<Guid> Add(Domain.Aggregates.Volunteer.Volunteer volunteer, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Aggregates.Volunteer.Volunteer>> GetByEmail(
        Email email,
        CancellationToken cancellationToken = default);
    
    Task<Result<Domain.Aggregates.Volunteer.Volunteer>> GetByPhoneNumber(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default);
}