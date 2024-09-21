using Application.Extensions;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Volunteers.Create;

public class CreateVolunteerService(
    IVolunteerRepository volunteerRepository, 
    IValidator<CreateVolunteerCommand> validator,
    ILogger<CreateVolunteerService> logger)
{
    public async Task<Result<Guid>> ExecuteAsync(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var email = Email.Create(command.Email);

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber);

        var volunteerEmail = await volunteerRepository
            .GetByEmailAsync(email.Value, cancellationToken);
        if (volunteerEmail.IsSuccess)
            return Errors.Volunteer.ValueIsAlreadyExists("email");

        var volunteerPhoneNumber = await volunteerRepository
            .GetByPhoneNumberAsync(phoneNumber.Value, cancellationToken);
        if (volunteerPhoneNumber.IsSuccess)
            return Errors.Volunteer.ValueIsAlreadyExists("phone number");
        
        var volunteerId = VolunteerId.NewVolunteerId();

        var fullName = FullName.Create(command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Patronymic);

        var description = Description.Create(command.Description);

        var yearsExperience = YearsExperience.Create(command.YearsExperience);

        var socialNetworks = new ValueObjectList<SocialNetwork>(command.SocialNetworks
            .Select(s => new SocialNetwork(Name.Create(s.Name).Value,
                Link.Create(s.Link).Value)));

        var requisites = new ValueObjectList<Requisite>(command.Requisites
            .Select(r => new Requisite(Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        var volunteer = new Domain.Aggregates.Volunteer.Volunteer(
            volunteerId,
            fullName.Value,
            email.Value,
            description.Value,
            yearsExperience.Value,
            phoneNumber.Value,
            socialNetworks,
            requisites);

        await volunteerRepository.AddAsync(volunteer, cancellationToken);

        logger.LogInformation("Created volunteer with id {volunteerId}", volunteerId.Value);

        return volunteer.Id.Value;
    }
}