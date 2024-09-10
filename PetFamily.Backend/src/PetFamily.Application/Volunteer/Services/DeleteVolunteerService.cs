using Application.Extensions;
using Application.Volunteer.Requests;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Volunteer.Services;

public class DeleteVolunteerService(
    IVolunteerRepository volunteerRepository,
    IValidator<DeleteVolunteerRequest> validator,
    ILogger<DeleteVolunteerRequest> logger)
{
    public async Task<Result<Guid>> ExecuteAsync(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await volunteerRepository
            .GetByIdAsync(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {request.VolunteerId}");
        
        var result = await volunteerRepository.DeleteAsync(volunteerResult.Value, cancellationToken);
        
        logger.LogInformation("Deleted volunteer with id {volunteerId}", request.VolunteerId);

        return result;
    }
}