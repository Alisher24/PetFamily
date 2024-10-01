using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Volunteers.UpdateRequisites;

public class UpdateRequisitesService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdateRequisitesCommand> validator,
    ILogger<UpdateRequisitesService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, UpdateRequisitesCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        UpdateRequisitesCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var requisites = new ValueObjectList<Requisite>(command.Requisites
            .Select(r => new Requisite(Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        volunteerResult.Value.UpdateRequisites(requisites);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Updated volunteer requisites with id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}