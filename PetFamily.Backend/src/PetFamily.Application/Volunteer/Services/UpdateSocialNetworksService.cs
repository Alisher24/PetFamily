using Application.Extensions;
using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonFields;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Volunteer.Services;

public class UpdateSocialNetworksService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdateSocialNetworksRequest> validator,
    ILogger<UpdateSocialNetworksRequest> logger)
{
    public async Task<Result<Guid>> ExecuteAsync(
        UpdateSocialNetworksRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return Errors.General.NotFound("volunteerId");

        var socialNetworks = new SocialNetworkList(request.SocialNetworks
            .Select(s => new SocialNetwork(Name.Create(s.Name).Value,
                Link.Create(s.Link).Value)));

        volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

        var result = await volunteerRepository.SaveAsync(volunteerResult.Value, cancellationToken);
        
        logger.LogInformation("Updated volunteer social networks with id {volunteerId}", request.VolunteerId);

        return result;
    }
}