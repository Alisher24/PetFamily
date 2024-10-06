using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.VolunteerManagement.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdService(
    IReadDbContext readDbContext,
    IValidator<GetVolunteerByIdQuery> validator)
    : IQueryService<VolunteerDto, GetVolunteerByIdQuery>
{
    public async Task<Result<VolunteerDto>> ExecuteAsync(GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerQuery = readDbContext.Volunteers;

        var volunteerResult = await volunteerQuery
            .FirstOrDefaultAsync(v => v.Id == query.Id, cancellationToken);
        if (volunteerResult is null)
            return Errors.General.NotFound($"volunteer with id: {query.Id}");

        return volunteerResult;
    }
}