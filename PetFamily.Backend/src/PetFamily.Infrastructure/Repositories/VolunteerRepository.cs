using Application.Volunteer;
using CSharpFunctionalExtensions;
using Domain.Models.Shared;
using Domain.Models.Volunteer;
using Domain.Models.Volunteer.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VolunteerRepository(ApplicationDbContext dbContext) : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Guid> Add(Volunteer volunteer, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, 
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Email == email,
                cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(email.Value);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByPhoneNumber(PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber,
                cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(phoneNumber.Value);

        return volunteer;
    }
}