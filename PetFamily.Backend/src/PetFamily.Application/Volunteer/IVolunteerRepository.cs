using CSharpFunctionalExtensions;
using Domain.Models.Shared;
using Domain.Models.Volunteer.ValueObjects;

namespace Application.Volunteer;

public interface IVolunteerRepository
{
    Task<Guid> Add(Domain.Models.Volunteer.Volunteer volunteer, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Models.Volunteer.Volunteer, Error>> GetByEmail(
        Email email,
        CancellationToken cancellationToken = default);
    
    Task<Result<Domain.Models.Volunteer.Volunteer, Error>> GetByPhoneNumber(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default);
}