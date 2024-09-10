using Application.Extensions;
using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonFields;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Volunteer.Services;

public class UpdateRequisitesService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdateRequisitesRequest> validator,
    ILogger<UpdateRequisitesRequest> logger)
{
    public async Task<Result<Guid>> ExecuteAsync(
        UpdateRequisitesRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {request.VolunteerId}");

        var requisites = new RequisiteList(request.Requisites
            .Select(r => new Requisite(Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        volunteerResult.Value.UpdateRequisites(requisites);

        var result = await volunteerRepository.SaveAsync(volunteerResult.Value, cancellationToken);
        
        logger.LogInformation("Updated volunteer requisites with id {volunteerId}", request.VolunteerId);

        return result;
    }
}