using Application.Volunteer;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VolunteerRepository(ApplicationDbContext dbContext) : IVolunteerRepository
{
    public async Task<Guid> Add(Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Result<Volunteer>> GetByEmail(Email email,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Email == email,
                cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(email.Value);

        return volunteer;
    }

    public async Task<Result<Volunteer>> GetByPhoneNumber(PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber,
                cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(phoneNumber.Value);

        return volunteer;
    }
}