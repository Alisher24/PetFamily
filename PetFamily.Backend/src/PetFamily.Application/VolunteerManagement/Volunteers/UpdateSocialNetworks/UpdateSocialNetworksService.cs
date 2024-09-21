using Application.Database;
using Application.Extensions;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdateSocialNetworksCommand> validator,
    ILogger<UpdateSocialNetworksService> logger,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> ExecuteAsync(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var socialNetworks = new ValueObjectList<SocialNetwork>(command.SocialNetworks
            .Select(s => new SocialNetwork(Name.Create(s.Name).Value,
                Link.Create(s.Link).Value)));

        volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

        await unitOfWork.SaveChanges(cancellationToken);
        
        logger.LogInformation("Updated volunteer social networks with id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}