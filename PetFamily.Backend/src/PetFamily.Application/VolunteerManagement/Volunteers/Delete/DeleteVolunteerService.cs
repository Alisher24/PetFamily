using Application.Database;
using Application.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Volunteers.Delete;

public class DeleteVolunteerService(
    IVolunteerRepository volunteerRepository,
    IValidator<DeleteVolunteerCommand> validator,
    ILogger<DeleteVolunteerService> logger,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> ExecuteAsync(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");
        
        volunteerResult.Value.Delete();

        await unitOfWork.SaveChanges(cancellationToken);
        
        logger.LogInformation("Deleted volunteer with id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}