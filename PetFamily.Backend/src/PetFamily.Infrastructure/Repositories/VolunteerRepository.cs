using Application.VolunteerManagement;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VolunteerRepository(WriteDbContext dbContext) : IVolunteerRepository
{
    public async Task<Guid> AddAsync(Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        return volunteer.Id.Value;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        dbContext.Volunteers.Remove(volunteer);

        return volunteer.Id.Value;
    }

    public Guid Save(Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        dbContext.Volunteers.Attach(volunteer);

        return volunteer.Id.Value;
    }

    public async Task<Result<Volunteer>> GetByIdAsync(VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId,
                cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId.Value.ToString());

        return volunteer;
    }

    public async Task<Result<Volunteer>> GetByEmailAsync(Email email,
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

    public async Task<Result<Volunteer>> GetByPhoneNumberAsync(PhoneNumber phoneNumber,
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